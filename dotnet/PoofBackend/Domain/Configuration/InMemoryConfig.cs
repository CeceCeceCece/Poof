﻿using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResource("roles", "User role(s)", new List<string> { "role" }),
              new IdentityResource("position", "Your position", new List<string> { "position" }),
              new IdentityResource("country", "Your country", new List<string> { "country" })
          };

        public static List<TestUser> GetUsers() =>
          new List<TestUser>
          {
              new TestUser
              {
                  SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                  Username = "Mick",
                  Password = "MickPassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Mick"),
                      new Claim("family_name", "Mining"),
                      new Claim("address", "Street 4"),
                      new Claim("role", "admin"),
                      new Claim("position", "Administrator"),
                      new Claim("country", "USA")
                  }
              },
              new TestUser
              {
                  SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                  Username = "Jane",
                  Password = "JanePassword",
                  Claims = new List<Claim>
                  {
                      new Claim("given_name", "Jane"),
                      new Claim("family_name", "Downing"),
                      new Claim("address", "Street 4"),
                      new Claim("role", "nemadmin"), 
                      new Claim("position", "Viewer"),
                      new Claim("country", "USA")
                  }
              }
          };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "company-employee",
                    ClientSecrets = new [] { new Secret("codemazesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi" },
                    RequireConsent = true,
                    AllowOfflineAccess = true
                },
               new Client
               {
                    ClientName = "MVC Client",
                    ClientId = "mvc-client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5010/signin-oidc" },
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile },
                    ClientSecrets = { new Secret("MVCSecret".Sha512()) },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5010/signout-callback-oidc" },
                    RequireConsent = true
               }
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { 
                new ApiScope("companyApi", "CompanyEmployee API"),
                new ApiScope("offline_access") 
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("companyApi", "CompanyEmployee API")
                {
                    Scopes = { "companyApi" }
                }
            };
    }
}