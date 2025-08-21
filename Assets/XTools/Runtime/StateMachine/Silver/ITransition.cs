namespace XTools.SM.Silver {
    public interface ITransition {
        IState to { get; }
        IPredicate condition { get; }
    }
}