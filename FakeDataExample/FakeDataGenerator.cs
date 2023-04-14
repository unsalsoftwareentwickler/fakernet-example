namespace FakeDataExample
{
    public class FakeDataExample
    {
        // Generate User
        public static List<User> GenerateUser(int count)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                var user = new User
                {
                    firstName = Faker.Name.First(),
                    lastName = Faker.Name.Last(),
                    country = Faker.Address.Country(),
                    city = Faker.Address.City(),
                    gsm = Faker.Phone.Number(),
                    username = Faker.Internet.UserName(),
                    email = Faker.Internet.Email(),
                    region = "",
                    passport = Faker.Identification.UsPassportNumber(),
                    birthDate = Faker.Identification.DateOfBirth(),
                    imagePath = Faker.Internet.Url(),

                    // Company
                    companyName = "GeneralCompany",
                    departmentName = "",
                    departmentCode = "",
                    jobName = "",
                    jobCode = "",
                    positionName = "",
                    positionCode = "",
                    storeName = "",
                    managerId = "",

                    // Booleans
                    isActive = Faker.Boolean.Random(),
                    isAdmin = false,
                    isSuperAdmin = false,
                    isManager = false,
                    isDirector = false,
                    isCeo = false,
                };

                users.Add(user);
            }
            return users;
        }
    }
}
