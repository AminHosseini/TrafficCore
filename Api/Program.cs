using Api.Bootstrapper.CensusAggregate;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Api;

var builder = WebApplication.CreateBuilder(args);

Settings.Configuration = builder.Configuration;


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IsarBootstrapper.Configure(builder.Services);

builder.Services.AddAuthentication(options =>
{
    //Sets cookie authentication scheme
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})

            .AddCookie(cookie =>
            {
                //Sets the cookie name and maxage, so the cookie is invalidated.
                cookie.Cookie.Name = "keycloak.cookie";
                cookie.Cookie.MaxAge = TimeSpan.FromMinutes(60);
                cookie.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                cookie.SlidingExpiration = true;
            })
            .AddOpenIdConnect(options =>
            {
                /*
                 * ASP.NET core uses the http://*:5000 and https://*:5001 ports for default communication with the OIDC middleware
                 * The app requires load balancing services to work with :80 or :443
                 * These needs to be added to the keycloak client, in order for the redirect to work.
                 * If you however intend to use the app by itself then,
                 * Change the ports in launchsettings.json, but beware to also change the options.CallbackPath and options.SignedOutCallbackPath!
                 * Use LB services whenever possible, to reduce the config hazzle :)
                */

                //Use default signin scheme
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //Keycloak server
                options.Authority = builder.Configuration.GetSection("Keycloak")["ServerRealm"];
                //Keycloak client ID
                options.ClientId = builder.Configuration.GetSection("Keycloak")["ClientId"];
                //Keycloak client secret
                options.ClientSecret = builder.Configuration.GetSection("Keycloak")["ClientSecret"];
                //Keycloak .wellknown config origin to fetch config
                options.MetadataAddress = builder.Configuration.GetSection("Keycloak")["Metadata"];
                //Require keycloak to use SSL
                options.RequireHttpsMetadata = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                //Save the token
                options.SaveTokens = true;
                //Token response type, will sometimes need to be changed to IdToken, depending on config.
                options.ResponseType = OpenIdConnectResponseType.Code;
                //SameSite is needed for Chrome/Firefox, as they will give http error 500 back, if not set to unspecified.
                options.NonceCookie.SameSite = SameSiteMode.Unspecified;
                options.CorrelationCookie.SameSite = SameSiteMode.Unspecified;

                // There has been reported issues where token is reported as expired right after login while keycloak returned expiration value is good 
                // Setting 'ValidateLifetime = false' below may fix the issue
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = ClaimTypes.Role,
                    ValidateIssuer = true,
                    ValidateLifetime = true
                };

            });

/*
 * For roles, that are defined in the keycloak, you need to use ClaimTypes.Role
 * You also need to configure keycloak, to set the correct name on each token.
 * Keycloak Admin Console -> Client Scopes -> roles -> Add mapper -> By configuration -> User Client Role
 * Mapper Type: "User Client Role"
 * Name: "role client mapper" or whatever you prefer
 * Multivalued: True
 * Token Claim Name: role
 * Add to access token: True
 */

/*
 * Policy based authentication
 */

builder.Services.AddAuthorization(options =>
{
    //Create policy with more than one claim
    options.AddPolicy("users", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
            (c.Value == "user") || (c.Value == "admin"))));
    //Create policy with only one claim
    options.AddPolicy("admins", policy =>
        policy.RequireClaim(ClaimTypes.Role, "admin"));
    //Create a policy with a claim that doesn't exist or you are unauthorized to
    options.AddPolicy("noaccess", policy =>
        policy.RequireClaim(ClaimTypes.Role, "noaccess"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
