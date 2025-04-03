namespace DocumentApprovalWorkflow.Handlers;

public class EngineeringLead : Approver
{
    public EngineeringLead() : base("Engineering Lead", 30, new List<Department> { Department.Engineering }) { }
}