using System.ComponentModel;
using Microsoft.AspNetCore.SignalR;

public class Project{
    public int Id {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
}
public class Teacher{
    public int Id {get; set;}
    public string? Name {get; set;}
    public List<Project>? Projects {get; set;}
}
