namespace MIE {
    public interface ITransition {
        IState to { get; }
        IPredicate condition { get; }
    }
}