using System.Diagnostics.CodeAnalysis;
using Personal_Financial_WebApi.Data.Dtos;

namespace Personal_Financial_WebApi.Controllers
{
    public class MonthYearEqualityComparer:IEqualityComparer<MonthDto>
    {   
        public bool Equals(MonthDto x, MonthDto y)
        {
            return x.Month==x.Month && x.Year==y.Year;
        }

        public int GetHashCode([DisallowNull] MonthDto obj)
        {
            return obj.Month+'/'+obj.Year;
        }
    }
}