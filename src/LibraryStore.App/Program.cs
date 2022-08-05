using LibraryStore.App.Data;
using LibraryStore.App.Extensions;
using LibraryStore.Business.Interfaces;
using LibraryStore.Data.Context;
using LibraryStore.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LibraryStoreDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMvc(o =>
{
    string invalidValueMsg = "O valor preenchido é inválido para este campo.";
    string beNumericMsg = "O campo deve ser numérico.";
    string requiredValueMsg = "Este campo precisa ser preenchido.";
    string bodyRequiredMsg = "É necessário que o body na requisição não esteja vazio.";

    o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => invalidValueMsg);
    o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => requiredValueMsg);
    o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => requiredValueMsg);
    o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => bodyRequiredMsg);
    o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => invalidValueMsg);
    o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => invalidValueMsg);
    o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => beNumericMsg);
    o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => invalidValueMsg);
    o.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => invalidValueMsg);
    o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => beNumericMsg);
    o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => requiredValueMsg);
});

builder.Services.AddScoped<LibraryStoreDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyValidationAttributeAdapterProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var defaultCulture = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(defaultCulture),
    SupportedCultures = new List<CultureInfo> { defaultCulture },
    SupportedUICultures = new List<CultureInfo> { defaultCulture },
};
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
