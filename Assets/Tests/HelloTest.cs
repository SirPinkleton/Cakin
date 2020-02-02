using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class HelloTest
    {
        ////////////////////// SETUP //////////////////////
        //[SetUp]
        //public void ResetScene() => EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);

        ////////////////////// TESTS //////////////////////
        // A Test behaves as an ordinary method
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator HelloTestWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        public void BasicTest()
        {
            bool isActive = false;

            Assert.AreEqual(false, isActive);
        }

        [Test]
        public void ErrorTest()
        {
            GameObject dummy = new GameObject("dummy");
            Assert.Throws<MissingComponentException>(
                () => dummy.GetComponent<Rigidbody2D>().velocity = Vector2.one
            );
        }

        //void RetryAction()
    }

    public class MovementTests
    {
        [UnityTest]
        public IEnumerator CanJump()
        {
            SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");

            yield return new WaitForSeconds(0.1f);

            var player = GameObject.Find("Player");

            //get initial place
            var initialHeight = player.transform.position.y;
            Debug.Log($@"initial height: {initialHeight}");
            yield return new WaitForSeconds(0.1f);

            //press space
            var script = player.GetComponent<Platformer>();
            script.pressingJump = true;

            //allow time to pass, for part of jump to happen
            yield return new WaitForSeconds(0.2f);

            //player should be higher
            var newHeight = player.transform.position.y;
            Debug.Log($@"new height: {newHeight}");
            Assert.True(newHeight > initialHeight);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TurnSpeedTest()
        {
            yield return null;
        }

        [TearDown]
        public void AfterEveryTest()
        {
            //foreach (var object in GameObject.Find(everything))
            //{Object.Destroy(object)}
        }
    }
}
