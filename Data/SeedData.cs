using Microsoft.AspNetCore.Identity;    
using InstrumentKaHealth.Models;

namespace InstrumentKaHealth.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            // Create roles
            string[] roleNames = { "SuperUser", "DataEntry", "ViewOnly" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Create admin user
            var adminEmail = "admin@company.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Administrator",
                    Department = "IT",
                    EmailConfirmed = true,
                    IsActive = true
                };

                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, "SuperUser");
            }

            // Add sample instruments if none exist
            if (!context.Instruments.Any())
            {
                var sampleInstruments = new List<Instrument>
                {
                    new Instrument
                    {
                        Name = "Oscilloscope XYZ-2000",
                        SerialNumber = "OSC-2023-001",
                        Location = "Lab 1",
                        Department = "Electronics",
                        Status = "Operational",
                        LastMaintenanceDate = DateTime.Now.AddMonths(-2),
                        NextMaintenanceDate = DateTime.Now.AddMonths(4),
                        CreatedBy = adminUser.Id,
                        CreatedAt = DateTime.Now
                    },
                    new Instrument
                    {
                        Name = "Spectrum Analyzer SA-5000",
                        SerialNumber = "SA-2023-002",
                        Location = "Lab 2",
                        Department = "RF Testing",
                        Status = "Operational",
                        LastMaintenanceDate = DateTime.Now.AddMonths(-1),
                        NextMaintenanceDate = DateTime.Now.AddMonths(5),
                        CreatedBy = adminUser.Id,
                        CreatedAt = DateTime.Now
                    }
                };

                context.Instruments.AddRange(sampleInstruments);
                await context.SaveChangesAsync();
            }
        }
    }
}
