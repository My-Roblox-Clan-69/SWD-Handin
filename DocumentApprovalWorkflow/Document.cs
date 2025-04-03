namespace DocumentApprovalWorkflow;

public enum Department
{
    HR,
    Finance,
    Engineering,
    Executive
}

public enum Urgency
{
    Low,
    Medium,
    High
}

public class Document
{
    public string Title { get; }
    public int ImportanceLevel { get; }
    public Department Department { get; }
    public Urgency Urgency { get; }

    public Document(string title, int importanceLevel, Department department, Urgency urgency)
    {
        Title = title;
        ImportanceLevel = importanceLevel;
        Department = department;
        Urgency = urgency;
    }
}