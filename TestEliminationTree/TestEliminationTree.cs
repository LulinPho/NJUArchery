using Archery_Competition_Webserver.Basement;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDataReader
{
    [TestClass]
    public class TestEliminationTree
    {
        private ILogger LocLogger { get; set; } 

        [TestInitialize]
        public void Init()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            LocLogger = factory.CreateLogger("TestEliminationTree");
            LocLogger.LogInformation("Test class created");
        }

        /**
         * This method tests the generation of elimination tree with given number of players.
         * The Ids of the players does not really care..
         */ 
        [TestMethod]
        [DataRow(4)]
        [DataRow(16)]
        [DataRow(12)]
        [DataRow(48)]
        public void TestEliminationTreeGeneration(int numPlayers)
        {
            LocLogger.LogInformation($"TestEliminationTreeGeneration: numPlayers={numPlayers}");
            List<int> playerIds=new List<int>();
            for (int i = 0; i < numPlayers; i++)
            {
                playerIds.Add(i);
            }

            EliminationTree tree=new EliminationTree();
            tree.GenerateEliminationTree(playerIds);

            // Test the nodes is either leave node or has two child nodes;
            // also check the ranks of the nodes
            Assert.IsTrue(CheckNodeChildrenRec(tree.Root));

            Assert.IsTrue(CheckFirstRoundPlayers(tree, playerIds));

            SimulatedCompetition(tree);
        }

        private bool CheckNodeChildrenRec(ITreeNode<EliminationPlayer> root)
        {
            if (root.LeftChildNode == null && root.RightChildNode == null)
            {
                return true;
            }
            else if (root.LeftChildNode == null || root.RightChildNode == null)
            {
                LocLogger.LogError("Left or right child is not null but the other is");
                return false;
            }

            if (root.LeftChildNode.Content.Rank >= root.RightChildNode.Content.Rank)
            {
                LocLogger.LogError($"Left child rank is greater or equal to the right one");
                return false;
            }

            bool res = true;
            res =res && CheckNodeChildrenRec(root.LeftChildNode);
            res = res && CheckNodeChildrenRec(root.RightChildNode);
            return res;
        }

        /**
         * Check that the player Ids in the first round is exactly the input players
         */ 
        private bool CheckFirstRoundPlayers(EliminationTree tree, IList<int> playerIds)
        {
            var matchPlayers = tree.GetMatchPlayers();
            List<int> players = new List<int>();
            foreach (var p in matchPlayers)
            {
                players.Add(p.PlayerA.Content.Id);
                players.Add(p.PlayerB.Content.Id);
            }

            List<int> playerIdsSort = new List<int>(playerIds);   // Copy construct
            playerIdsSort.Sort();
            players.Sort();
            return playerIdsSort.SequenceEqual(playerIds);
        }

        /**
         * Do a simulated competition. I.e. for each match, the winner is picked randomly.
         * The matches are performed until only one player remains in the tree.
         * Currently, only test that the matches could be end for finite time.
         */ 
        private void SimulatedCompetition(EliminationTree tree)
        {
            var matches = tree.GetBottomMatchPlayers();
            var rng = new Random();
            while (matches.Count > 0)
            {
                foreach(var m in matches)
                {
                    int winnerId = rng.Next(1) == 0? m.PlayerA.Content.Id : m.PlayerB.Content.Id;
                    tree.UpdateMatchResult(m.PlayerA, m.PlayerB, winnerId);
                }
                matches = tree.GetBottomMatchPlayers();
            }
        }
    }
}