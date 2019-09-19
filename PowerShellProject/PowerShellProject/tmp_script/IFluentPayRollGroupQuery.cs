public interface IFluentPayRollGroupQuery
{
        Task<PayRollGroup> MapToEntity(PayRollGroupView inputObject);
        Task<List<PayRollGroup>> MapToEntity(List<PayRollGroupView> inputObjects);
    
        Task<PayRollGroupView> MapToView(PayRollGroup inputObject);
        Task<NextNumber> GetNextNumber();
	Task<PayRollGroupView> GetViewById(long payRollGroupId);
	Task<PayRollGroupView> GetViewByNumber(long payRollGroupNumber);
}
