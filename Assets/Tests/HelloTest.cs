using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
}
