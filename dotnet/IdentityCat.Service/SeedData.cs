﻿using System.Security.Claims;
using IdentityCat.Domain;
using IdentityCat.Persistence;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace com.b_velop.IdentityCat.Service;

public class SeedData
{
    public static void EnsureSeedData(
        WebApplication app)
    {
        using var scope = app
            .Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        var context = scope.ServiceProvider.GetService<UserDbContext>();
        context.Database.Migrate();
        
        var cmd = new User.CreateUserCommand("marcel", "Pass123$", "marcel@test.de", "Benders", "Marcel");
        var user = User.Create(cmd);

        var c1 = new Claim("scope", "pbi_admin");
        var c2 =
            new Claim("scope", "openid");
        var c3 =
            new Claim("scope", "profile");
        user.Claims.AddRange(new HashSet<Claim>
        {
            c1, c2, c3
        });
        context.Users.Add(user);
        // context.UserClaims.AddRange(
        //     new UserClaim
        //     {
        //         User = user,
        //         Type = "scope",
        //         Value = "pbi_admin"
        //     }, new UserClaim
        //     {
        //         User = user,
        //         Type = "scope",
        //         Value = "openid",
        //     }, new UserClaim
        //     {
        //         User = user,
        //         Type = "scope",
        //         Value = "profile",
        //     });
        context.SaveChanges();
        scope.Dispose();

        // var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        // var alice = userMgr.FindByNameAsync("alice")
        //     .Result;
        // if (alice == null)
        // {
        //     alice = new ApplicationUser
        //     {
        //         UserName = "alice",
        //         Email = "AliceSmith@email.com",
        //         EmailConfirmed = true
        //     };
        //     var result = userMgr.CreateAsync(alice, "Pass123$")
        //         .Result;
        //     if (!result.Succeeded)
        //         throw new Exception(result.Errors.First()
        //             .Description);
        //
        //     result = userMgr.AddClaimsAsync(alice, new Claim[]
        //         {
        //             new(JwtClaimTypes.Name, "Alice Smith"),
        //             new(JwtClaimTypes.GivenName, "Alice"),
        //             new(JwtClaimTypes.FamilyName, "Smith"),
        //             new(JwtClaimTypes.Scope, "pbi_admin"),
        //             new(JwtClaimTypes.WebSite, "http://alice.com")
        //         })
        //         .Result;
        //     if (!result.Succeeded)
        //         throw new Exception(result.Errors.First()
        //             .Description);
        //
        //     Log.Debug("alice created");
        // }
        // else
        // {
        //     Log.Debug("alice already exists");
        // }
        //
        // var bob = userMgr.FindByNameAsync("bob")
        //     .Result;
        // if (bob == null)
        // {
        //     bob = new ApplicationUser
        //     {
        //         UserName = "bob",
        //         Email = "BobSmith@email.com",
        //         EmailConfirmed = true
        //     };
        //     var result = userMgr.CreateAsync(bob, "Pass123$")
        //         .Result;
        //     if (!result.Succeeded)
        //         throw new Exception(result.Errors.First()
        //             .Description);
        //
        //     result = userMgr.AddClaimsAsync(bob, new Claim[]
        //         {
        //             new(JwtClaimTypes.Name, "Bob Smith"),
        //             new(JwtClaimTypes.GivenName, "Bob"),
        //             new(JwtClaimTypes.FamilyName, "Smith"),
        //             new(JwtClaimTypes.WebSite, "http://bob.com"),
        //             new("location", "somewhere")
        //         })
        //         .Result;
        //     if (!result.Succeeded)
        //         throw new Exception(result.Errors.First()
        //             .Description);
        //
        //     Log.Debug("bob created");
        // }
        // else
        // {
        //     Log.Debug("bob already exists");
        // }
    }
}