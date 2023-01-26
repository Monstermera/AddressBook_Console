using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using AddressBook.Models;
using AddressBook.Service;
using Newtonsoft.Json;

namespace AddressBook.Services;

internal class Menu
{
    private List<Contact> Contacts = new List<Contact>();
    private readonly FileService file = new FileService();

    public Menu()
    {
        PopulateContactsList();
    }

    public void PopulateContactsList()
    {
        try
        {
            var items = JsonConvert.DeserializeObject<List<Contact>>(file.Read());
            if (items != null)
                Contacts = items;
        }
        catch { }
    }

    public void WelcomeMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the address book");
        Console.WriteLine("Please choose from one of the following options: \n");
        Console.WriteLine("1. Add a new contact");
        Console.WriteLine("2. Show all contacts");
        Console.WriteLine("3. Show one specific contact");
        Console.WriteLine("4. Delete one specific contact");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1": 
                CreateOne(); 
                break;
            case "2": 
                ShowAll(); 
                break;
            case "3": 
                ShowOne(); 
                break;
            case "4":
                DeleteOne();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Not an option. \n\nPress any key to return to the menu");
                Console.ReadKey();
                break;
        }
    }

    //CREATE A CONTACT
    private void CreateOne()
    {
        Console.Clear();
        Console.WriteLine("Add a new contact: \n ");

        Contact contact = new Contact();
        Console.Write("Enter first name: ");
        contact.FirstName = Console.ReadLine() ?? "";
        Console.Write("Enter last name: ");
        contact.LastName = Console.ReadLine() ?? "";
        Console.Write("Enter e-mail: ");
        contact.Email = Console.ReadLine() ?? "";
        Console.Write("Enter phone number: ");
        contact.PhoneNumber = Console.ReadLine() ?? "";
        Console.Write("Enter street address: ");
        contact.StreetAddress = Console.ReadLine() ?? "";
        Console.Write("Enter zip code: ");
        contact.ZipCode = Console.ReadLine() ?? "";
        Console.Write("Enter city: ");
        contact.City = Console.ReadLine() ?? "";

        Contacts.Add(contact);
        Console.Clear();
        Console.WriteLine("Contact added.");
        Console.ReadKey();

        file.Save(JsonConvert.SerializeObject(Contacts));
    }

    //SHOW ALL CONTACTS
    private void ShowAll()
    {
        Console.Clear();
        Console.WriteLine("List of contacts\n");

        if (Contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Address book is empty");        
        } 
        else
        {
            foreach (Contact contact in Contacts)
            {
                Console.WriteLine($"First name: {contact.FirstName}");
                Console.WriteLine($"Last name: {contact.LastName}");
                Console.WriteLine($"Email: {contact.Email} \n");
            } 
        }
        Console.ReadKey();
    }

    //SHOW ONE SPECIFIC CONTACT
    private void ShowOne()
    {
        if (Contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Address book is empty");
        }
        else
        {
            Console.Clear();
            Console.Write("First name of the contact you want to find: ");
            var findFirstName = Console.ReadLine().ToLower();
            Console.Write("Last name of the contact you want to find: ");
            var findLastName = Console.ReadLine().ToLower();
            foreach (Contact contact in Contacts)
            {
                var FullName = Contacts.FirstOrDefault(x => x.FirstName == findFirstName && x.LastName == findLastName);

                if (FullName != null)
                {
                    Console.Clear();
                    Console.WriteLine($"First name: {findFirstName}");
                    Console.WriteLine($"Last name: {findLastName}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Phone number: {contact.PhoneNumber}");
                    Console.WriteLine($"Address: {contact.StreetAddress}{","} {contact.ZipCode} {contact.City}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("There are no contact with the given name");
                }
            }
        }
        Console.ReadKey();
 
        //Console.Clear();
        //Console.WriteLine("Enter the first name of the contact you want to find\n");

        //if (Contacts.Count == 0)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Address book is empty");
        //}
        //else
        //{
        //    var FirstName = Console.ReadLine();
        //    foreach (Contact contact in Contacts) 
        //    {
        //        if (FirstName == contact.FirstName)
        //        {
        //            Console.Clear();
        //            Console.WriteLine($"First name: {contact.FirstName}");
        //            Console.WriteLine($"Last name: {contact.LastName}");
        //            Console.WriteLine($"Email: {contact.Email}");
        //            Console.WriteLine($"Phone number: {contact.PhoneNumber}");
        //            Console.WriteLine($"Address: {contact.StreetAddress}{","} {contact.ZipCode} {contact.City}");
        //        }
        //        else
        //        {
        //            Console.Clear();
        //            Console.WriteLine("There are no contact with the given name");
        //        }
        //    }
        //}
        //Console.ReadKey();
    }

    //DELETE ONE SPECIFIC CONTACT
    private void DeleteOne()
    {

        if (Contacts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Address book is empty");
            Console.ReadKey();
        }
        else
        {
            Console.Clear();
            Console.Write("First name of the contact you want to delete: ");
            var FirstName = Console.ReadLine();

             if (FirstName == null) 
            {
                Console.Clear();
                Console.WriteLine("There are no contact with the given name");
                Console.ReadKey();
            } else 
            Console.Write("Last name of the contact you want to delete: ");
            var LastName = Console.ReadLine();
            var contact = Contacts.FirstOrDefault(x => x.FirstName.ToLower() == FirstName.ToLower() && x.LastName.ToLower() == LastName.ToLower());

            if (contact == null) 
            {
                Console.Clear();
                Console.WriteLine("There are no contact with the given name");
                Console.ReadKey();
            } 

            if (contact != null)
            {
                Console.WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName}? Press Y for yes and N for no");
                var answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    Contacts.Remove(contact);
                    Console.Clear();
                    Console.WriteLine($"{contact.DisplayName} was removed from the address book");
                    Console.ReadKey();
                }
                else if (answer == "n")
                {
                    Console.Clear();
                    Console.WriteLine($"{contact.DisplayName} remains in the address book");
                    Console.ReadKey();
                }
            }
        }                
        file.Save(JsonConvert.SerializeObject(Contacts));
    }



    
    //    Console.Clear();
    //    Console.Write("First name of the contact you want to delete: ");

    //    if (Contacts.Count == 0)
    //    {
    //        Console.Clear();
    //        Console.WriteLine("Address book is empty");
    //        Console.ReadKey();
    //    }
    //    else
    //    {
    //       string FullName = Console.ReadLine();
    //       var contact = Contacts.FirstOrDefault(x => x.FirstName.ToLower() == FullName.ToLower());

    //        if (contact == null) 
    //        {
    //            Console.Clear();
    //            Console.WriteLine("There are no contact with the given name");
    //            Console.ReadKey();
    //        } 

    //        if (contact != null)
    //        {
    //            Console.WriteLine($"Are you sure you want to delete {contact.FirstName} {contact.LastName}? Press Y for yes and N for no");
    //            var answer = Console.ReadLine().ToLower();
    //            if (answer == "y")
    //            {
    //                Contacts.Remove(contact);
    //                Console.Clear();
    //                Console.WriteLine($"{contact.DisplayName} was removed from the address book");
    //                Console.ReadKey();
    //            }
    //            else if (answer == "n")
    //            {
    //                Console.Clear();
    //                Console.WriteLine($"{contact.DisplayName} remains in the address book");
    //                Console.ReadKey();
    //            }
    //        }
    //    }                
    //    file.Save(JsonConvert.SerializeObject(Contacts));
    //}
}


