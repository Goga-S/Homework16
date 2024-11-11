using Homework16.Abstraction;
using Homework16.Model;
using Newtonsoft.Json;

namespace Homework16
{
    public class FileOperations : IFileOperations
    {
        private string filePath = @"C:\Users\user\Desktop\Homeworks\Homework16\Homework16\Homework16\data\personrecords.json";

        private List<Person> readFile()
        {
            string content = File.ReadAllText(filePath);

            List<Person> recordList = string.IsNullOrEmpty(content)
            ? new List<Person>()
            : JsonConvert.DeserializeObject<List<Person>>(content) ?? new List<Person>();

            return recordList;
        }

        private void writeFile(List<Person> records)
        {
            var updatedJson = JsonConvert.SerializeObject(records, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }
        public void AddRecord(Person person)
        {
            var currentList = readFile();
            var pers = new Person() 
            {
                PersonId = person.PersonId,
                FirstName = person.FirstName,
                LastName = person.LastName,
                CreateDate = DateTime.Now,
                Salary = person.Salary,
                WorkExperience = person.WorkExperience,
                Jobposition = person.Jobposition,
                PersonAdress = new Adress()
                {
                    City = person.PersonAdress.City,
                    Country = person.PersonAdress.Country,
                    HomeNumber = person.PersonAdress.HomeNumber,
                }
            };

           currentList.Add(pers);

           writeFile(currentList); 

        }

        public string DeleteRecord(string id)
        {
           var list = readFile();
            if (list.Count > 0)
            {
                var record = list.FirstOrDefault(r => r.PersonId == id);
                if (record != null)
                {
                    list.Remove(record);
                    writeFile(list);
                    return id;
                }
                else
                {
                    return "No records for that ID";
                }
            } else
            {
                return "No records in database";
            }
        }

        public List<Person>? GetAllRecords()
        {
            var list = readFile();
            if (list.Count > 0)
            {
                return list;
            } else
            {
                return null;
            }
        }

        public Person? GetRecord(string id)
        {
            var list = readFile();
            if (list.Count > 0)
            {
                var record = list.FirstOrDefault(m => m.PersonId == id);
                if (record != null)
                {
                    return record;
                } else
                {
                    return null;
                }

            } else
            {
                return null;
            }
            

        }

        public List<Person>? GetSpecificRecords(Jobposition jobposition)
        {
            var records = readFile();
            if (records.Count > 0 )
            {
                var filteredRecords = records.Where(m => m.Jobposition == jobposition).ToList();
                if (filteredRecords.Count > 0)
                {
                    return filteredRecords;
                } else
                {
                    return null;
                }

            } else
            {
                return null;
            }
            

            
        }
         
        public string UpdateRecord(Person person)
        {
            var records = readFile();
            if (records.Count > 0)
            {
                
                var index = records.IndexOf(records.FirstOrDefault(m => m.PersonId == person.PersonId));
                if (index>= 0)
                {
                    records[index].FirstName = person.FirstName != "" ? person.FirstName : records[index].FirstName;
                    records[index].LastName = person.LastName != ""? person.LastName : records[index].LastName;
                    records[index].Salary = person.Salary != 0 ? person.Salary : records[index].Salary;
                    records[index].WorkExperience = person.WorkExperience != 0 ? person.WorkExperience : records[index].WorkExperience;
                    records[index].PersonAdress.City = person.PersonAdress.City != "" ? person.PersonAdress.City : records[index].PersonAdress.City;
                    records[index].PersonAdress.Country = person.PersonAdress.Country != "" ? person.PersonAdress.Country : records[index].PersonAdress.Country;
                    records[index].PersonAdress.HomeNumber = person.PersonAdress.HomeNumber != "" ? person.PersonAdress.HomeNumber : records[index].PersonAdress.HomeNumber;
                    writeFile(records);
                    return person.PersonId;
                }
                else
                {
                    return "no record for that person";
                }
            }else
            {
                return "No records in database";
            }

        }
    }
}
