using ImageMagick;
using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace MCBA_Web.Services;

public class CustomerService : ICustomerService
{
    private readonly MCBAContext _context;

    public CustomerService(MCBAContext context)
    {
        _context = context;
    }

    

    public void UpdateProfilePicture(Customer customer)
    {
        using (MagickImage image = new MagickImage(customer.ImageUpload.OpenReadStream()))
        {
            // Resize appropriately
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            if (imageWidth != imageHeight)
            {
                double aspectRatio = (double)imageWidth / (double)imageHeight;
                //resize the image
                if (aspectRatio == 1)
                {
                    image.Resize(400, 400);
                }
                else
                {
                    if (imageWidth > imageHeight)
                    {
                        image.Resize(400, (int)(400 / aspectRatio));
                    }
                    else
                    {
                        image.Resize((int)(400 * aspectRatio), 400);
                    }
                }
            }
            else
            {
                image.Resize(400, 400);
            }

            // Resize-Reformat image
            image.Format = MagickFormat.Jpg;

            // Convert to byte array
            byte[] imageData = image.ToByteArray();

            // Update customer 
            customer.ProfilePictureContentType = customer.ImageUpload.ContentType;
            customer.ProfilePicture = imageData;
            customer.HasDefaultProfilePicture = false;

            // Save
            _context.Customer.Update(customer);
            _context.SaveChanges();

        }
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customer.ToList();
    }

    public Customer GetById(int id)
    {
        Console.WriteLine(id);
        return _context.Customer
            .Include(m => m.Address)
            .Include(x => x.Accounts)
            .ThenInclude(t => t.Transactions)
            .FirstOrDefault(t => t.CustomerID == id);
    }

    public void Add(Customer customer)
    {
        _context.Add(customer);
        _context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _context.Update(customer);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Remove(id);
        _context.SaveChanges();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

}