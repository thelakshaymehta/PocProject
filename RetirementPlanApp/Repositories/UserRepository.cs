using Newtonsoft.Json;
using RetirementPlanApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

public class UserRepository
{
    private readonly string _filePath = @"L:\rps\RetirementPlanApp\App_Data\users.json";

    public List<User> GetAll()
    {
        var jsonData = File.ReadAllText(_filePath);
        return JsonConvert.DeserializeObject<List<User>>(jsonData);
    }

    public User GetByUsernameAndPassword(string username, string password)
    {
        var users = GetAll();
        return users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
}
