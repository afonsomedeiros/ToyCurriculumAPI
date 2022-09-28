namespace CurriculumAPI.Models;
public class Curriculum
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Phone { get; private set; }
    public string Objective { get; private set; }

    public Curriculum(string name, string address, string phone, string objective){
        Name = name;
        Address = address;
        Phone = phone;
        Objective = objective;
    }
}