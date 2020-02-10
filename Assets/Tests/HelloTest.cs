using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

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
        }

        [UnityTest]
        public IEnumerator TurnSpeedTest()
        {
            SceneManager.LoadScene("Assets/Scenes/SampleScene.unity");

            yield return new WaitForSeconds(0.1f);

            var player = GameObject.Find("Player");
            var script = player.GetComponent<Platformer>();
            //move to the right, full speed, for a few frames
            script.pressingRight = true;
            script._rigidbody.velocity = new Vector2(script._maxSpeed, 0);
            yield return new WaitForSeconds(0.2f);
            
            //get initial place
            var positionBeforeChange = player.transform.position.x;
            Debug.Log($@"position before movement is swapped: {positionBeforeChange}");
            script.pressingRight = false;
            script.pressingLeft = true;

            //wait one frame
            yield return null;
            
            //we've just changed momentum, but how much?
            var positionAfterChange = player.transform.position.x;
            Debug.Log($@"position after movement is swapped: {positionAfterChange}");

            var differenceInChange = Math.Abs(positionAfterChange - positionBeforeChange);
            Debug.Log($@"difference from before and after change: {differenceInChange}");

            //player's momentum shouldn't be too strong
            Assert.True(differenceInChange < 0.11f);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            //foreach (var object in GameObject.Find(everything))
            //{Object.Destroy(object)}
        }
    }

    public class MenuTests
    {
        public List<string> allSceneNames = new List<string>();

        [SetUp]
        public void BeforeEveryTest()
        {
            var sceneFiles = System.IO.Directory.GetFiles("Assets/Scenes");

            //starting at 1, 0 is index of test scene, not an actual scene
            foreach (var sceneFile in sceneFiles)
            {
                if (sceneFile.Contains(".meta"))
                {
                    continue;
                }
                //assets/scenes\mainmenu.unity
                Debug.Log($"currently looking as scene file {sceneFile}");

                //mainmenu.unity
                string sceneFileName = sceneFile.Split('\\').Last();
                Debug.Log($"scene filename found: {sceneFileName}");

                //mainmenu
                string sceneName = sceneFileName.Split('.').First();
                Debug.Log($"scene name recorded: {sceneName}");
                allSceneNames.Add(sceneName);
            }

            allSceneNames = allSceneNames.Distinct().ToList();

            foreach(var thing in allSceneNames)
            {
                Debug.Log($"scene found: {thing}");
            }
        }

        //for any level we'd run this test for, check if the options menu exists
        [UnityTest]
        public IEnumerator LevelHasOptionsPanel()
        {
            foreach (var sceneName in allSceneNames)
            {
                Debug.Log($"loading scene: {sceneName}");
                SceneManager.LoadScene(sceneName);
                yield return new WaitForSeconds(0.1f);

                //can't use GameObject.Find() as that skips over inactive elements like this menu
                var allCurrentSceneObjects = Resources.FindObjectsOfTypeAll<GameObject>();

                //search through all game objects to find the one for options panel
                bool optionsFound = false;
                foreach (var item in allCurrentSceneObjects)
                {
                    if (item.name == "OptionsPanel")
                    {
                        optionsFound = true;
                        break;
                    }
                }

                Assert.True(optionsFound, $"Options panel doesn't exist for current scene ({sceneName}), user cannot change options");
            }
        }

        //doesn't make sense for the main menu, but for other levels, make sure the pause panel exists
        [UnityTest]
        public IEnumerator LevelHasPauseMenu()
        {
            //need to add each scene to build settings for test to function
            foreach (var sceneName in allSceneNames)
            {
                if (sceneName == "MainMenu")
                {
                    Debug.Log("main menu doesn't require pause menu, ignoring");
                    continue;
                }
                Debug.Log($"loading scene: {sceneName}");
                SceneManager.LoadScene(sceneName);
                yield return new WaitForSeconds(0.1f);
                
                //can't use GameObject.Find() as that skips over inactive elements like this menu
                var allCurrentSceneObjects = Resources.FindObjectsOfTypeAll<GameObject>();

                //search through all game objects to find the one for options panel
                bool pauseFound = false;
                foreach (var item in allCurrentSceneObjects)
                {
                    if (item.name == "PausePanel")
                    {
                        pauseFound = true;
                        break;
                    }
                }

                Assert.True(pauseFound, $"Pause panel doesn't exist for a scene ({sceneName}), user cannot pause/quit/etc.");
            }
        }
    }
}
