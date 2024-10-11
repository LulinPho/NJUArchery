using System.ComponentModel.DataAnnotations;

namespace Archery_Competition_Webserver.Basement
{

    public struct EliminationPlayer
    {
        public int Id { get; set; }
        public int Rank { get; set; }

        public EliminationPlayer(int id, int rank)
        {
           Id = id;
           Rank = rank;
        }
    }
    public class RobinTreeNode : ITreeNode<EliminationPlayer>
    {
        public EliminationPlayer Content { get; set; }
        public ITreeNode<EliminationPlayer>? Parent { get; set; }
        public ITreeNode<EliminationPlayer>? LeftChildNode { get; set; }
        public ITreeNode<EliminationPlayer>? RightChildNode { get; set; }

        public RobinTreeNode(EliminationPlayer content, ITreeNode<EliminationPlayer>? parent = null, ITreeNode<EliminationPlayer>? leftChildNode = null, ITreeNode<EliminationPlayer>? rightChildNode = null)
        {
            Content = content;
            Parent = parent;
            LeftChildNode = leftChildNode;
            RightChildNode = rightChildNode;
        }
    }

    public class EliminationTree : ITree<EliminationPlayer>
    {
        [Required]
        public ITreeNode<EliminationPlayer> Root { get; set; } = new RobinTreeNode(new EliminationPlayer(0,0));
        public IList<ITreeNode<EliminationPlayer>> TreeNodes { get; set; } = new List<ITreeNode<EliminationPlayer>>();

        public int Depth { get; set; }
        public int Width { get; set; }

        public EliminationTree() { }
        public EliminationTree(ITreeNode<EliminationPlayer> root, IList<ITreeNode<EliminationPlayer>> treeNodes, int depth, int width)
        {
            Root = root;
            TreeNodes = treeNodes;
            Depth = depth;
            Width = width;
        }

        public EliminationTree(int rootIndex, IList<ITreeNode<EliminationPlayer>> treeNodes)
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
