using LibraryManagementSystem.Factories;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IBookFactory, BookFactory>() 
            .AddSingleton<IBookRepository, BookRepository>() 
            .AddSingleton<IBookManagementService, BookManagementService>()
            .AddSingleton<IUserRepository, UserRepository>() 
            .AddSingleton<IUserManagementService, UserManagementService>()
            .BuildServiceProvider();

        var bookManagementService = serviceProvider.GetService<IBookManagementService>();
        var userManagementService = serviceProvider.GetService<IUserManagementService>();

        if (bookManagementService == null || userManagementService == null)
        {
            Console.WriteLine("Failed to resolve required services.");
            return;
        }

        var libraryManagementService = LibraryManagementService.Instance(bookManagementService, userManagementService);

        libraryManagementService.DisplayOptions();
    }
}
