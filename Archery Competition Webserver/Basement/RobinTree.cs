using System.ComponentModel.DataAnnotations;

namespace Archery_Competition_Webserver.Basement
{

    public struct RobinPlayer
    {
        public int Id { get; set; }
        public int Rank { get; set; }

        public RobinPlayer(int id, int rank)
        {
           Id = id;
           Rank = rank;
        }
    }
    public class RobinTreeNode : ITreeNode<RobinPlayer>
    {
        public RobinPlayer Content { get; set; }
        public ITreeNode<RobinPlayer>? Parent { get; set; }
        public ITreeNode<RobinPlayer>? LeftChildNode { get; set; }
        public ITreeNode<RobinPlayer>? RightChildNode { get; set; }

        public RobinTreeNode(RobinPlayer content, ITreeNode<RobinPlayer>? parent = null, ITreeNode<RobinPlayer>? leftChildNode = null, ITreeNode<RobinPlayer>? rightChildNode = null)
        {
            Content = content;
            Parent = parent;
            LeftChildNode = leftChildNode;
            RightChildNode = rightChildNode;
        }
    }

    public class RobinTree : ITree<RobinPlayer>
    {
        [Required]
        public ITreeNode<RobinPlayer> Root { get; set; } = new RobinTreeNode(new RobinPlayer(0,0));
        public IList<ITreeNode<RobinPlayer>> TreeNodes { get; set; } = new List<ITreeNode<RobinPlayer>>();

        public int Depth { get; set; }
        public int Width { get; set; }

        public RobinTree() { }
        public RobinTree(ITreeNode<RobinPlayer> root, IList<ITreeNode<RobinPlayer>> treeNodes, int depth, int width)
        {
            Root = root;
            TreeNodes = treeNodes;
            Depth = depth;
            Width = width;
        }

        public RobinTree(int rootIndex, IList<ITreeNode<RobinPlayer>> treeNodes)
        {
            Root = treeNodes[rootIndex];
            TreeNodes = treeNodes;
        }

        public void GenerateRobinTree()
        {
            int count = TreeNodes.Count;

            int roundNum = 0;
            while (count > 0)
            {
                count >>= 1;
                roundNum++;
            }


        }

        public int GetDepth()
        {
            return Depth;
        }

        public int GetWidth()
        {
            return Width;
        }

    }
}
