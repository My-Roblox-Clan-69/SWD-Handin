namespace DocumentApprovalWorkflow;

public class ApproverFactory : IApproverFactory
{
    public IApprover CreateChain()
    {
        var hrManager = new Handlers.HRManager();
        var financeDirector = new Handlers.FinanceDirector();
        var engineeringLead = new Handlers.EngineeringLead();
        var ceo = new Handlers.CEO();

        hrManager.SetNext(financeDirector);
        financeDirector.SetNext(ceo);

        return new DepartmentRoutingApprover(hrManager, financeDirector, engineeringLead, ceo);
    }
}

public class DepartmentRoutingApprover : IApprover
{
    private readonly IApprover _hrManager;
    private readonly IApprover _financeDirector;
    private readonly IApprover _engineeringLead;
    private readonly IApprover _ceo;

    public DepartmentRoutingApprover(IApprover hrManager, IApprover financeDirector, IApprover engineeringLead, IApprover ceo)
    {
        _hrManager = hrManager;
        _financeDirector = financeDirector;
        _engineeringLead = engineeringLead;
        _ceo = ceo;
    }

    public void SetNext(IApprover nextApprover)
    {
        // Not used, as this is the entry point
    }

    public void ProcessRequest(ApprovalRequest request)
    {
        IApprover startingApprover = request.Document.Department switch
        {
            Department.HR => _hrManager,
            Department.Finance => _financeDirector,
            Department.Engineering => _engineeringLead,
            _ => _ceo
        };
        startingApprover.ProcessRequest(request);
    }
}