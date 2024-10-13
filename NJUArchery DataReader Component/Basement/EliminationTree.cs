using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

    public class EliminationTreeNode : ITreeNode<EliminationPlayer>
    {
        public EliminationPlayer Content { get; set; }
        public ITreeNode<EliminationPlayer>? Parent { get; set; }
        public ITreeNode<EliminationPlayer>? LeftChildNode { get; set; }
        public ITreeNode<EliminationPlayer>? RightChildNode { get; set; }

        public EliminationTreeNode(EliminationPlayer content, ITreeNode<EliminationPlayer>? parent = null, 
            ITreeNode<EliminationPlayer>? leftChildNode = null, ITreeNode<EliminationPlayer>? rightChildNode = null)
        {
            Content = content;
            Parent = parent;
            LeftChildNode = leftChildNode;
            RightChildNode = rightChildNode;
        }
    }

    /**
     * The struct for each pair of the players that would take part in a match game.
     */
    public struct MatchPlayersInfo
    {
        public int Layer { get; set; }
        public ITreeNode<EliminationPlayer> PlayerA { get; set; }
        public ITreeNode<EliminationPlayer> PlayerB { get; set; }
    }

    public class EliminationTree : ITree<EliminationPlayer>
    {
        [Required]
        public ITreeNode<EliminationPlayer> Root { get; set; } = new EliminationTreeNode(new EliminationPlayer(-1,-1));
        public IList<ITreeNode<EliminationPlayer>> TreeNodes { get; set; } = new List<ITreeNode<EliminationPlayer>>();
        public IList<int>? PlayerIds { get; set; }

        /**
         * The depth of the binary tree, defined as the number of layers (excluding the root).
         */ 
        public int Depth { get; set; }

        /**
         * The width of the binary tree, defined as the number of players.
         */ 
        public int Width { get; set; }

        public EliminationTree() { }
        public EliminationTree(ITreeNode<EliminationPlayer> root, IList<ITreeNode<EliminationPlayer>> treeNodes, 
            int depth, int width, IList<int>? playerIds)
        {
            Root = root;
            TreeNodes = treeNodes;
            Depth = depth;
            Width = width;
            PlayerIds = playerIds;
        }

        public EliminationTree(int rootIndex, IList<ITreeNode<EliminationPlayer>> treeNodes)
        {
            Root = treeNodes[rootIndex];
            TreeNodes = treeNodes;
        }

        /**
         * MXY: Generate the Elimination match tree.
         * Precondition: 
         * (1) The Root have already been set.
         * (2) The input playerIds is a list of the players that participate in this competition, **in their ranks**
         * (typically determined by qualification match) from first to last.
         * Postconditions: 
         * (1) The tree is built for the elimination match, with the bottom layer being filled with player ranks
         * and ids, and other layers only filled with ranks. The ranks are one-based indexes.
         * (2) The tree must be a "full" binary tree which is complete with only (possible) vacancies at bottom layer;
         * (3) The number of leave nodes is equal to the input size of playerIds.
         * (4) All the nodes (apart from the root one) are discarded.
         * (5) Each node must either be a leave node or have two children nodes; for the latter case, the left child
         * node must have rank LESS than the right one.
         */ 
        public void GenerateEliminationTree(IList<int> playerIds)
        {
            int layer_id = 0;   // Index of layer; the root layer is 0
            int current_layer_full_size = 1;

            Root.Content = new EliminationPlayer(-1, 1);
            List<ITreeNode<EliminationPlayer>> lastLayerNodes = [Root];

            // Discard the other nodes if any
            TreeNodes = [Root];
            Root.LeftChildNode = null;
            Root.RightChildNode = null;

            // Loop invariant: each loop iteration processes the layer indexed by layer_id
            while (true)
            {
                List<ITreeNode<EliminationPlayer>> currentLayerNodes = [];

                layer_id++;
                current_layer_full_size *= 2;

                for (int i=0; i< lastLayerNodes.Count; i++)
                {
                    var par_node = lastLayerNodes[i];
                    int left_rank = par_node.Content.Rank;
                    int right_rank = current_layer_full_size + 1 -  lastLayerNodes[i].Content.Rank;
                    if (right_rank <= playerIds.Count)
                    {
                        // This is a valid match pair, add these nodes
                        var leftNode = new EliminationTreeNode(new EliminationPlayer(-1, left_rank), 
                            parent:par_node);
                        var rightNode = new EliminationTreeNode(new EliminationPlayer(-1, right_rank),
                            parent:par_node);
                        par_node.LeftChildNode = leftNode;
                        par_node.RightChildNode = rightNode;
                        currentLayerNodes.Add(leftNode);
                        currentLayerNodes.Add(rightNode);
                        TreeNodes.Add(leftNode);
                        TreeNodes.Add(rightNode);
                    }else
                    {
                        // The `par_node` is actually a leave node
                        int rank = par_node.Content.Rank;
                        int node_player_id = playerIds[rank - 1];
                        par_node.Content = new EliminationPlayer(node_player_id, par_node.Content.Rank);
                    }
                }

                lastLayerNodes = currentLayerNodes;
                if (current_layer_full_size >= playerIds.Count)
                {
                    break;
                }
            }

            // Fill the playerIds for the leave nodes
            foreach(var node in lastLayerNodes)
            {
                // The nodes of the last layer; must all be leave and not proceeed by the previous iteration.
                int rank = node.Content.Rank;
                node.Content = new EliminationPlayer(playerIds[rank - 1], rank);
            }

            // Now we fill the data for this
            Depth = layer_id;
            Width = playerIds.Count;
            PlayerIds = playerIds;
        }

        /**
         * *This API may be changed layer*
         * Update the match result of two players represented by the given two nodes.
         * These two nodes must belong to the same parent.
         * The winnerId must match one of the two nodes.
         */ 
        public void UpdateMatchResult(EliminationTreeNode leftNode, EliminationTreeNode rightNode, int winnerId)
        {
            // sanity checks first
            if (leftNode.Parent == null || rightNode.Parent == null || 
                leftNode.Parent != rightNode.Parent)
            { 
                throw new InvalidDataException("Two nodes have null or different parents");
            }

            if (leftNode.Content.Id != winnerId && rightNode.Content.Id != winnerId)
            {
                throw new InvalidDataException("The winner is neither of the two nodes");
            }

            int par_rank = leftNode.Parent.Content.Rank;
            leftNode.Parent.Content = new EliminationPlayer(winnerId, par_rank);
        }

        /**
         * Returns the player pairs for which match game will be taken place, i.e. the pairs
         * (that belonging to the same parent) with results not determined yet.
         */ 
        public List<MatchPlayersInfo> GetMatchPlayers()
        {
            List<MatchPlayersInfo> result = [];
            GetMatchPlayersRec(result, Root, 0);
            return result;
        }

        /**
         * Returns the player pairs that belong to the bottom layer (with maximal layer id).
         * Note that the players for matches may belong to two layers, typically at the beginning of the
         * elimination matches, for example, when the total number of players is 12.
         */  
        public List<MatchPlayersInfo> GetBottomMatchPlayers()
        {
            var result = GetMatchPlayers();
            if (result.Count == 0)
                return result;
            // Select the maximal id
            int maxLayer = result.MaxBy(p => p.Layer).Layer;
            return result.Where(p => p.Layer == maxLayer).ToList();
        }

        private void GetMatchPlayersRec(List<MatchPlayersInfo> result, ITreeNode<EliminationPlayer> root,  int currentLayer)
        {
            if (root == null)
            {
                return;
            }
            if (root.Content.Id >= 0)
            {
                return;
            }
            if (root.LeftChildNode == null && root.RightChildNode == null)
            {
                // This is leave node; nothing to do.
                return;
            }
            else if (root.LeftChildNode == null || root.RightChildNode == null)
            {
                throw new InvalidDataException("Only one of Left or Right child is not null");
            }

            // Now, left and right are not null
            if (root.Content.Id < 0)
            {
                if (root.LeftChildNode.Content.Id >= 0 && root.RightChildNode.Content.Id >= 0)
                {
                    result.Add(new MatchPlayersInfo
                    {
                        Layer = currentLayer + 1,
                        PlayerA = root.LeftChildNode,
                        PlayerB = root.RightChildNode
                    });
                }
                else
                {
                    // All undetermined; recursion
                    GetMatchPlayersRec(result, root.LeftChildNode, currentLayer + 1);
                    GetMatchPlayersRec(result, root.RightChildNode, currentLayer + 1);
                }
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
