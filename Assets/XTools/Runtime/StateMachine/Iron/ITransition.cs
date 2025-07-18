namespace XTools.SM.Iron {
    public interface ITransition {
        IState to { get; }
        IPredicate condition { get; }
    }
}