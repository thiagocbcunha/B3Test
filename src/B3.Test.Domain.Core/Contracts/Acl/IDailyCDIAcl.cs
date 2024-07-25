using B3.Test.Domain.Core.Model;

namespace B3.Test.Domain.Core.Contracts.Acl;

public interface IDailyCdiAcl
{
    Task<IEnumerable<FeeModel>> GetFees();
}