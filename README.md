# Faker.NET - Fake User Data Generation and Database Transfer
- Used in the Project <br>
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white) - v2022<br>
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)<br>
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) - 6.0<br>
![MySQL](https://img.shields.io/badge/mysql-%2300f.svg?style=for-the-badge&logo=mysql&logoColor=white)

# Installation

<h3>Database</h3>
<ul>
  <li>Import the MySQL database in the repository to your own database</li>
  <ul>
    <li><strong>MySQL Database Name:</strong> fakeusersdb</li>
    <li><strong>MySQL Database Username:</strong> root (default)</li>
    <li><strong>MySQL Database Password:</strong> none (default)</li>
    <li><strong>MySQL Table Name:</strong> user</li>
  </ul>
</ul>

# Usage
<ol>
  <li>Open the project with your code editor</li>
  <li>You can change the MySQL connection string on line 5 in <strong>Program.cs</strong></li>
  <li>Enter the number of records you want to be created into the GenerateUser method on lines 12</li>
  <li>Run the program</li>
  <li>The number of records you specify will be added to the database</li>
</ol>
