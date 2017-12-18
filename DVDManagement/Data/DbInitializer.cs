using DVDManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVDManagement.Data
{
        public class DbInitializer
        {

        public static async Task InitializeAsync(DVDMAGContext context, UserManager<Admin> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
            context.Database.EnsureCreated();

            //如果已經有管理者帳號則不用建立
            if (context.Admin.Any())
            {
                return;
            }

            await CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger);
        }

        private static async Task CreateDefaultUserAndRoleForApplication(UserManager<Admin> um, RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger)
        {
            const string administratorRole = "Administrator";
            const string email = "hank1234@gmail.com";
            const string password = "Hank@1234";

            //await CreateDefaultAdministratorRole(rm, logger, administratorRole);
            var user = await CreateDefaultUser(um, logger, email);
            await SetPasswordForDefaultUser(um, logger, email, password, user);
            //await AddDefaultRoleToDefaultUser(um, logger, email, administratorRole, user);
        }

        private static async Task CreateDefaultAdministratorRole(RoleManager<IdentityRole> rm, ILogger<DbInitializer> logger, string administratorRole)
        {
            logger.LogInformation($"Create the role `{administratorRole}` for application");
            var ir = await rm.CreateAsync(new IdentityRole(administratorRole));
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created the role `{administratorRole}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default role `{administratorRole}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task<Admin> CreateDefaultUser(UserManager<Admin> um, ILogger<DbInitializer> logger, string email)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");
            var user = new Admin
            {
                UserName = "hank",
                Email = email,
                PhoneNumber = "0987654321"
            };

            var ir = await um.CreateAsync(user);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Default user `{email}` cannot be created");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }

            var createdUser = await um.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForDefaultUser(UserManager<Admin> um, ILogger<DbInitializer> logger, string email, string password, Admin user)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            IdentityResult ir = await um.AddPasswordAsync(user, password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<Admin> um, ILogger<DbInitializer> logger, string email, string administratorRole, Admin user)
        {
            logger.LogInformation($"Add default user `{email}` to role '{administratorRole}'");
            var ir = await um.AddToRoleAsync(user, administratorRole);
            if (ir.Succeeded)
            {
                logger.LogDebug($"Added the role '{administratorRole}' to default user `{email}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{email}`");
                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
                throw exception;
            }
        }

        private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        {
            string errors = null;
            foreach (var identityError in ir.Errors)
            {
                errors += identityError.Description;
                errors += ", ";
            }
            return errors;
        }
    }
}
