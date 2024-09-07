

namespace api.Interfaces
{


    public interface IQuery<P, R>
    {
        Task<R> Query(P parameterObject);
    }

    public interface ICommand<P>
    {
        Task Execute(P parameterObject);
    }

}