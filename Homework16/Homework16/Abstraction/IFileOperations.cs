using Homework16.Model;

namespace Homework16.Abstraction
{
    public interface IFileOperations
    {
        void AddRecord(Person person);
        string DeleteRecord(string id);
        string UpdateRecord(Person person);
        List<Person>? GetAllRecords();
        Person? GetRecord(string id);
        List<Person>? GetSpecificRecords(Jobposition position);

    }
}
