# Currency Exchanger**
Web project with using Backend Technology : ASP.NET Core MVC , Entity Framework, PostreSQL Server. Frontend Technology : HTML , CSS , Bootstrap 5

#### **The layout is not responsive. It is recommended to use on a scale of 100-125%. The main task in the development of deepening back-end skills**

## Instruction

- Copy link for clone 

![image](https://user-images.githubusercontent.com/94395710/169912069-febb86d6-17bf-4f18-a154-dca0e1cf9db6.png)
- Make a clone in Visual Studio IDE

![image](https://user-images.githubusercontent.com/94395710/169912425-10823149-3ca1-437f-9bad-2b9336c37c7d.png)
- When you load the project you will see its structure

![image](https://user-images.githubusercontent.com/94395710/169912700-df9f4120-a2a3-455e-88eb-8b018055a199.png)
- Next, to start the project, we need to set up a connection string to the Database.To do this, find the file `appsettings.json`.

![image](https://user-images.githubusercontent.com/94395710/169912994-af356d93-9136-4419-bc01-24164d245fa0.png)
- There we have to set the line `"DefaultConnection"`.To do this, you need to replace the existing values `Host`,`Port`,`Database`,`Username`,`Password` with your own.

![image](https://user-images.githubusercontent.com/94395710/169913260-f77fb6e3-c612-4037-b815-8f4f7f9dea76.png)
- After we set up the initial administrator data. To do this, in the `Domain` folder
go to `AppDbContext.сs`

![image](https://user-images.githubusercontent.com/94395710/169915075-c5c6244a-1bc3-4dd9-9058-891bd560a235.png)
- :exclamation::exclamation::exclamation: **You will use this information to log in as an administrator.**

```C#
var admin = new User()
{
     Id = 1,
     FirstName = "Bill", //Enter your first name
     LastName = "Tomson", //Enter your last name
     Email = "Example@gmail.com", //Enter your email
     Age = 33, //Enter your age
     Password = HashingService.GetHashString("12345Qwerty"), //Enter your password in GetHashString("....")
     RoleId = adminRole.Id
};
```
- Now we can do the migration and init the database. To do this, we need to open `Package Manager Console` and in turn enter `Add-migration [name of migration]` , `Update-database`. After that, you will create a migrations folder, as well as all the tables in the database.
#### :white_check_mark: Now you can launch and use the Web-Application :white_check_mark:

# **Functional**
## An unauthorized user can:
- View ContactUs Page where you can contact the administrator
- Register
- LogIn

![image](https://user-images.githubusercontent.com/94395710/169918004-94f1dcf1-e89c-4777-8210-05228021edd5.png)

## After registration, the user can:
- View ContactUs Page where you can contact the administrator
- View the exchange rate for a specific date
- Make an exchange with the currency you need
- View your currency exchange history
- LogOut

![image](https://user-images.githubusercontent.com/94395710/169918157-c6b7bca7-041c-43ce-91c6-ab7f1194dd26.png)

## If you are logged in as an administrator, you can view messages from users:
-To do this, you need to enter the following route in the url line - `/show/message`

![image](https://user-images.githubusercontent.com/94395710/169918887-f15f9731-aa34-4ee4-96b7-fb14ee271589.png)
