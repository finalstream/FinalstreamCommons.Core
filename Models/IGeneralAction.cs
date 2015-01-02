namespace FinalstreamCommons.Models
{
    public interface IGeneralAction<T>
    {
        void Invoke(T param);
    }
}