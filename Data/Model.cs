using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace BlazorProject.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Wish> Wishes { get; set; }
    public DbSet<Accommodation> Accommodations { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertySet> PropertySets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Customer>().HasData(Seed.Customers);
        //modelBuilder.Entity<Location>().HasData(Seed.Locations);
        //modelBuilder.Entity<PropertySet>().HasData(Seed.PropertySets);
        //modelBuilder.Entity<Accommodation>().HasData(Seed.Accommodations);
        //modelBuilder.Entity<Booking>().HasData(Seed.Bookings);
        //modelBuilder.Entity<Wish>().HasData(Seed.Wishes);
        //modelBuilder.Entity<Property>().HasData(Seed.Properties);
    }
}

public class Customer
{
    public int CustomerId { get; set; }
    
    [Required(ErrorMessage = "Voornaam is vereist.")]
    [StringLength(50, ErrorMessage = "Voornaam mag niet langer dan 50 karakters zijn.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Achternaam is vereist.")]
    [StringLength(50, ErrorMessage = "Achternaam mag niet langer dan 50 karakters zijn.")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is vereist.")]
    [EmailAddress(ErrorMessage = "Ongeldig e-mailadres.")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Telefoonnummer is vereist.")]
    [Phone(ErrorMessage = "Ongeldig telefoonnummer.")]
    [MinLength(5, ErrorMessage ="Ongeldig telefoonnummer.")]
    public string PhoneNumber { get; set; }

    public List<Booking> Bookings { get; set; }
    public List<Wish> Wishes { get; set; }
}

public class Booking
{
    public int BookingId { get; set; }
    public DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "Datum van aankomst is vereist.")]

    public DateTime ArrivalDate { get; set; }

    [Required(ErrorMessage = "Datum van vertrek is vereist.")]
    public DateTime DepartureDate { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Minstens 1 gast is vereist.")]
    public int NumberOfGuests { get; set; }

    public decimal TotalPrice { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int AccommodationId { get; set; }
    public Accommodation Accommodation { get; set; }
}



public class Wish
{
    public int WishId { get; set; }
    public string Value { get; set; }

    public int PropertySetId { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public PropertySet PropertySet { get; set; }
}

public class Accommodation
{
    public int AccommodationId { get; set; }

    [Required(ErrorMessage = "Naam is vereist.")]
    [StringLength(100, ErrorMessage = "Naam mag niet langer zijn dan 100 karakters.")]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Prijs is vereist.")]
    [Range(0, double.MaxValue, ErrorMessage = "Prijs moet een positief getal zijn.")]
    public decimal Price { get; set; }

    public string? ImageURL { get; set; }  // Make ImageURL nullable

    [Required(ErrorMessage = "Location is required.")]
    public int LocationId { get; set; }

    // Navigation properties
    public Location Location { get; set; }

    public List<Booking> Bookings { get; set; }
    public List<Property> Properties { get; set; }
}


public class Location
{
    public int LocationId { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }

    public List<Accommodation> Accommodations { get; set; }
}

public class Property
{
    public int Id { get; set; } // Primary key
    public int AccommodationId { get; set; }
    public Accommodation Accommodation { get; set; }
    public int PropertySetId { get; set; }
    public PropertySet PropertySet { get; set; }
    public string Value { get; set; }
}


public class PropertySet
{
    public int PropertySetId { get; set; }
    public string Name { get; set; }

    public List<Property> Properties { get; set; }
}
