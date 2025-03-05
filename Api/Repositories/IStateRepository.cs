using Api.Models;

namespace Api.Repositories
{
    public interface IStateRepository
    {
        List<State> GetStates();
        State GetStateById(int id);
        void AddState(State_2 state);
        void UpdateState(State_2 state);
        string DeleteState(int id);
    }
}
