using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test
{
    #region Module
    public class TestSuite
    {
        private GameManager gameManager;
        private Player player;
        //Setup at the start of every test
        [SetUp]
        public void Setup()
        {
            //Load resoure first
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
            GameObject clone = Object.Instantiate(prefab);
            gameManager = clone.GetComponent<GameManager>();
            player = Object.FindObjectOfType<Player>();
        }
        #region Unit
        [UnityTest]
        public IEnumerator GameManagerWasLoaded()
        {
            //Wait for frame
            yield return new WaitForEndOfFrame();

            //Check if exists after frame
            Assert.IsTrue(gameManager != null);
        }
        [UnityTest]
        public IEnumerator PlayerHasRigidBody()
        {
            //Get rigidbody off player
            var rigid = player.GetComponent<Rigidbody>();
            //Wait for a frame
            yield return null;

            Assert.IsTrue(rigid != null);
        }
        [UnityTest]
        public IEnumerator PlayerExistsInGameManager()
        {
            //Wait for frame
            yield return new WaitForEndOfFrame();
            //Get player manager
            player = gameManager.GetComponentInChildren<Player>();
            
            Assert.IsTrue(player != null);
        }
        [UnityTest]
        public IEnumerator ItemCollidesWithPlayer()
        {
            //Get an item
            var item = Object.FindObjectOfType<Item>();
            //Position both in the same location
            player.transform.position = new Vector3(0, 2, 0);
            item.transform.position = new Vector3(0, 2, 0);
            yield return new WaitForSeconds(0.1f);
            //Assert that item should be destroyed
            Assert.IsTrue(item == null);
        }
        [UnityTest]
        public IEnumerator ItemCollectedRewardsOnePoint()
        {

            //Get an item
            var item = Object.FindObjectOfType<Item>();
            //Position both player and item in the same location
            player.transform.position = new Vector3(0, 2, 0);
            item.transform.position = new Vector3(0, 2, 0);
            //save old score
            var oldScore = gameManager.score;
            //wait for 0.1 seconds
            yield return new WaitForSeconds(0.1f);
            //Get score 
            var score = gameManager.score;

            Assert.IsTrue(score ==  oldScore + 1);
        }

        [UnityTest]
        public IEnumerator PlayerShootsItem()
        {

            //Get item
            Item item = Object.FindObjectOfType<Item>();
            //Set player position
            player.transform.position = new Vector3(0,3,-3);
            //Set item position
            item.transform.position = new Vector3(0, 3, 0);
            //Wait for 1 frame
            yield return null;

            Assert.IsTrue(player.Shoot());
        }
        #endregion
        //Destroy test object (destroys after every test)
        [TearDown]
        public void TearDown()
        {
            Object.Destroy(gameManager.gameObject);
        }
    }
    #endregion
}