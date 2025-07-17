namespace XTools {
    public interface IVisitor {
        void Visit<T>(T visitable) where T : IVisitable;
    }

    public interface IVisitable {
        void Accept(IVisitor visitor);
    }

    public interface IVisitorDataSupplier {
        void TrySupply<T>(T requester) where T : IVisitableDataRequester;
    }

    public interface IVisitableDataRequester {
    }
}