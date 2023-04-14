using MySql.Data.MySqlClient;
using System.Diagnostics;

// MySQL Connection Definitions
string connectionString = "server=localhost;database=fakeusersdb;port=3306;user=root;password=";
MySqlConnection connection = new MySqlConnection(connectionString);

// Counter Definitions
int userCounter = 0;

// Generate Definitions
var users = FakeDataExample.FakeDataExample.GenerateUser(500);

// Start Timer
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

string departmentNameString = "Department001";
string jobNameString = "Job001";
string positionNameString = "Position001";
string storeNameString = "Store001";
string regionNameString = "Region001";
int isManager = 0;
int isDirector = 0;
int isCeo = 0;

string managerIdString = "00000001";
int managerIdInt = int.Parse(managerIdString);

string regnumString = "00000000";
int regnumInt = int.Parse(regnumString);

int departmentCounter = 0;
int jobCounter = 0;
int positionCounter = 0;
int storeCounter = 0;
int regionCounter = 0;
int isManagerCounter = 19;
int isDirectorCounter = 0;
int isCeoCounter = 0;
int regnumCounter = 0;
//int managerIdCounter = 0;


// GENERATE GROUPS
foreach (var user in users)
{
    string birthDateFromFake = user.birthDate.ToString();
    string birthDateFormat = "d.MM.yyyy HH:mm:ss";
    DateTime date = DateTime.ParseExact(birthDateFromFake, birthDateFormat, System.Globalization.CultureInfo.InvariantCulture);
    string newFormat = "yyyy-MM-dd HH:mm:ss";
    
    // New MySQL Date Format
    string newBirthDate = date.ToString(newFormat);

    // başlangıç 19
    isManagerCounter++;
    if (isManagerCounter % 20 == 0)
    {
        isManager = 1;
        managerIdInt++;
        managerIdString = managerIdInt.ToString("D8"); // 00000002
    }
    else
    {
        isManager = 0;
    }

    foreach (var group in users.GroupBy(u => u.managerId))
    {
        bool isFirst = true;
        foreach (var u in group)
        {
            if (isFirst)
            {
                isFirst = false;
                u.managerId = "";
            }
        }
    }

    try
    {
        connection.Open();

        int isActiveBool = 0;
        int isAdminBool = 0;
        int isSuperadminBool = 0;

        if (user.isActive == true) { isActiveBool = 1; } else { }
        if (user.isAdmin == true){isAdminBool = 1;}else{}
        if (user.isSuperAdmin == true){ isSuperadminBool = 1;}else{}

        regnumInt++;
        regnumString = regnumInt.ToString("D8");

        string query = "INSERT INTO user (firstName, lastName, country, city, gsm, username, email, region, regNumber, birthDate, imagePath, companyName, departmentName, departmentCode, jobName, jobCode, positionName, positionCode, storeName, managerId, isActive, isAdmin, isSuperadmin, isManager, isDirector, isCeo) VALUES (@firstName, @lastName, @country, @city, @gsm, @username, @email, @region, @regNumber, @birthDate, @imagePath, @companyName, @departmentName, @departmentCode, @jobName, @jobCode, @positionName, @positionCode, @storeName, @managerId, @isActive, @isAdmin, @isSuperadmin, @isManager, @isDirector, @isCeo)";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("firstName", user.firstName);
        command.Parameters.AddWithValue("lastName", user.lastName);
        command.Parameters.AddWithValue("country", user.country);
        command.Parameters.AddWithValue("city", user.city);
        command.Parameters.AddWithValue("gsm", user.gsm);
        command.Parameters.AddWithValue("username", user.username);
        command.Parameters.AddWithValue("email", user.email);
        command.Parameters.AddWithValue("region", regionNameString);
        command.Parameters.AddWithValue("regNumber", regnumString);
        command.Parameters.AddWithValue("birthDate", newBirthDate);
        command.Parameters.AddWithValue("imagePath", user.imagePath);
        command.Parameters.AddWithValue("companyName", user.companyName);
        command.Parameters.AddWithValue("departmentName", departmentNameString);
        command.Parameters.AddWithValue("departmentCode", departmentNameString);
        command.Parameters.AddWithValue("jobName", jobNameString);
        command.Parameters.AddWithValue("jobCode", jobNameString);
        command.Parameters.AddWithValue("positionName", positionNameString);
        command.Parameters.AddWithValue("positionCode", positionNameString);
        command.Parameters.AddWithValue("storeName", storeNameString);
        command.Parameters.AddWithValue("managerId", managerIdString);
        command.Parameters.AddWithValue("isActive", isActiveBool);
        command.Parameters.AddWithValue("isAdmin", isAdminBool);
        command.Parameters.AddWithValue("isSuperadmin", isSuperadminBool);
        command.Parameters.AddWithValue("isManager", isManager);
        command.Parameters.AddWithValue("isDirector", isDirector);
        command.Parameters.AddWithValue("isCeo", isCeo);
        command.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Hata: " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
    userCounter++;
    departmentCounter++;
    jobCounter++;
    positionCounter++;
    storeCounter++;
    regionCounter++;
    isDirectorCounter++;

    if (departmentCounter % 20 == 0)
    {
        int nextDepartmentNameString = int.Parse(departmentNameString.Substring(10)) + 1;
        departmentNameString = $"Department{nextDepartmentNameString:D3}";
    }
    if (jobCounter % 20 == 0)
    {
        int nextJobNameString = int.Parse(jobNameString.Substring(3)) + 1;
        jobNameString = $"Job{nextJobNameString:D3}";
        
    }
    if(positionCounter % 20 == 0)
    {
        int nextPositionNameString = int.Parse(positionNameString.Substring(8)) + 1;
        positionNameString = $"Position{nextPositionNameString:D3}";
    }
    if (storeCounter % 20 == 0)
    {
        int nextStoreNameString = int.Parse(storeNameString.Substring(5)) + 1;
        storeNameString = $"Store{nextStoreNameString:D3}";
    }
    if (regionCounter % 100 == 0)
    {
        int nextRegionNameString = int.Parse(regionNameString.Substring(6)) + 1;
        regionNameString = $"Region{nextRegionNameString:D3}";
    }

}

// SET UP MANAGER ID'S
int affectedManagerRows = 0;
int affectedDirectorRows = 0;
int affectedManDirRows = 0;
try
{
    string commandText = "UPDATE user u1 " +
                 "JOIN (SELECT MIN(id) AS id, storeName " +
                 "FROM user " +
                 "GROUP BY storeName) u2 " +
                 "ON u1.id = u2.id " +
                 "JOIN user u3 " +
                 "ON u2.storeName = u3.storeName " +
                 "AND u1.id != u3.id " +
                 "SET u3.managerId = u1.regNumber " +
                 "WHERE u1.storeName = u2.storeName";
    MySqlCommand command = new MySqlCommand(commandText, connection);
    connection.Open();
    int affectedRows = command.ExecuteNonQuery();
    affectedManagerRows = affectedRows;
}
catch(Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    connection.Close();
}

// SET UP DIRECTORS
try
{
    string commandText = "UPDATE user t1 JOIN " +
             "(SELECT MAX(id) as maxid FROM user GROUP BY region) t2 " +
             "ON t1.id = t2.maxid SET t1.isDirector = 1;";
    MySqlCommand command = new MySqlCommand(commandText, connection);

    connection.Open();
    int affectedRows = command.ExecuteNonQuery();
    affectedDirectorRows = affectedRows;
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    connection.Close();
}

// SET MANAGER'S DIRECTORS
try
{
    string commandText = @"
        UPDATE user
        JOIN (
            SELECT region, MAX(CASE WHEN isDirector = 1 THEN regNumber END) AS directorRegNumber
            FROM user
            GROUP BY region
        ) AS grp ON user.region = grp.region
        SET user.managerId = grp.directorRegNumber
        WHERE user.isManager = 1;";

    MySqlCommand command = new MySqlCommand(commandText, connection);

    connection.Open();
    int affectedRows = command.ExecuteNonQuery();
    affectedManDirRows = affectedRows;
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
finally
{
    connection.Close();
}

// Stop Timer
stopwatch.Stop();
TimeSpan elapsed = stopwatch.Elapsed;

// Output
Console.WriteLine("==========> OUTPUT");
Console.WriteLine("==========> Elapsed time: " + elapsed);
Console.WriteLine("==========> Total updated Manager: " + affectedManagerRows);
Console.WriteLine("==========> Total updated Director: " + affectedDirectorRows);
Console.WriteLine("==========> Total updated Manager ID: " + affectedManDirRows);
Console.WriteLine("==========> Total generated Users: " + userCounter.ToString());
