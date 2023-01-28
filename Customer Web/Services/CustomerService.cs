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

    private Tuple<int, int> ResizeUploadedImage(int width, int height)
    {
        if (width != height)
        {
            double aspectRatio = (double)width / (double)height;
            
            //resize the image
            if (aspectRatio == 1)
            {
                return Tuple.Create(400, 400);
            }
            else
            {
                if (width > height)
                {
                    return Tuple.Create(400, (int)(400 / aspectRatio));
                }
                else
                {
                    return Tuple.Create((int)(400 * aspectRatio), 400);
                }
            }
        }
        else
        {
            return Tuple.Create(400, 400);
        }

        return Tuple.Create(width, height);
    }

    public void MakeDefaultProfilePicture(Customer customer)
    {
        customer.ProfilePicture = null;
        customer.HasDefaultProfilePicture = true;
        customer.ProfilePictureContentType = null;

        _context.Customer.Update(customer);
        _context.SaveChanges();
    }
    public void UpdateProfilePicture(Customer customer)
    {
        using (MagickImage image = new MagickImage(customer.ImageUpload.OpenReadStream()))
        {
            // Resize appropriately
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            Tuple<int,int> widthHeight = ResizeUploadedImage(imageWidth, imageHeight);

            image.Resize(widthHeight.Item1, widthHeight.Item2);

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

        return _context.Customer
            .Include(m => m.Address)
            .Include(x => x.Accounts)
            .ThenInclude(t => t.Transactions)
            .FirstOrDefault(t => t.CustomerID == id);
    }

    public void Add(Customer customer)
    {
        _context.Customer.Add(customer);
		_context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        _context.Customer.Update(customer);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _context.Remove(id);
    }

}