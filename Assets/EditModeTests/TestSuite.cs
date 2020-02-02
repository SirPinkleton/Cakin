using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;

namespace Tests
{
    public class TestSuite
    {
        public class MovementTests
        {
            [UnityTest]
            public IEnumerator CanJump()
            {
                EditorSceneManager.OpenScene("Assets/Scenes/SampleScene.unity");

                new WaitForSeconds(1);

                var player = GameObject.Find("Player");

                Debug.Log(player);

                //get initial place
                var initialHeight = player.transform.position.y;
                Debug.Log($@"initial height: {initialHeight}");

                //press space
                var script = player.GetComponent<Platformer>();
                script.pressingJump = true;

                //allow time to pass, for part of jump to happen
                new WaitForSeconds(0.5f);

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
}
