namespace Archery_Competition_Webserver.Basement
{


    public interface ITreeNode<T>
    {
        public T Content { get; set; }
        public abstract ITreeNode<T>? Parent { get; set; }
        public abstract ITreeNode<T>? LeftChildNode { get; set; }
        public abstract ITreeNode<T>? RightChildNode { get; set; }
    }

    public interface ITree<T>
    {
        public ITreeNode<T> Root { get; set; }
        public IList<ITreeNode<T>> TreeNodes { get; set; }

        public int Depth { get; set; }
        public int Width { get; set; }

    }
}
