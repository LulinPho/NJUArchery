using Archery_Competition_Webserver.Basement;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDataReader
{
    [TestClass]
    public class TestEliminationTree
    {
        private ILogger logger { get; set; } 

        [TestInitialize]
        public void Init()
        {
            var factory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = factory.CreateLogger("TestEliminationTree");
            logger.LogInformation("Test class created");
        }

        [TestMethod]
        public void TestMethod()
        {
            TestEliminationTreeGeneration(12);
        }

        /**
         * This method tests the generation of elimination tree with given number of players.
         * The Ids of the players does not really care..
         */ 
        [TestMethod]
        [DataRow(16)]
        [DataRow(12)]
        public void TestEliminationTreeGeneration(int numPlayers)
        {
            logger.LogInformation($"TestEliminationTreeGeneration: numPlayers={numPlayers}");
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

            // TODO: Check the playerIds in the bottom layer(s).
        }

        private bool CheckNodeChildrenRec(ITreeNode<EliminationPlayer> root)
        {
            if (root.LeftChildNode == null && root.RightChildNode == null)
            {
                return true;
            }
            else if (root.LeftChildNode == null || root.RightChildNode == null)
            {
                logger.LogError("Left or right child is not null but the other is");
                return false;
            }

            if (root.LeftChildNode.Content.Rank >= root.RightChildNode.Content.Rank)
            {
                logger.LogError($"Left child rank is greater or equal to the right one");
                return false;
            }

            bool res = true;
            res =res && CheckNodeChildrenRec(root.LeftChildNode);
            res = res && CheckNodeChildrenRec(root.RightChildNode);
            return res;
        }
    }
}