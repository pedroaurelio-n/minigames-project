using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace GameTests.Input.Touch
{
    public class TouchInputModelTests
    {
        class BaseTouchInputModelTests
        {
            protected IPhysicsProvider PhysicsProvider { get; private set; }
            protected LayerMaskOptions LayerMaskOptions { get; private set; }
            protected TapInputOptions TapInputOptions { get; private set; }
            protected SwipeInputOptions SwipeInputOptions { get; private set; }
            protected LongPressInputOptions LongPressInputOptions { get; private set; }
            protected TwoPointMoveInputOptions TwoPointMoveInputOptions { get; private set; }
            
            protected TouchInputModel Model { get; private set; }
            
            [SetUp]
            public void Setup ()
            {
                PhysicsProvider = Substitute.For<IPhysicsProvider>();
                LayerMaskOptions = new LayerMaskOptions();
                TapInputOptions = new TapInputOptions();
                SwipeInputOptions = new SwipeInputOptions();
                LongPressInputOptions = new LongPressInputOptions();
                TwoPointMoveInputOptions = new TwoPointMoveInputOptions();
                
                Model = new TouchInputModel(
                    PhysicsProvider,
                    LayerMaskOptions,
                    TapInputOptions,
                    SwipeInputOptions,
                    LongPressInputOptions,
                    TwoPointMoveInputOptions
                );
            }

            class Tap : BaseTouchInputModelTests
            {
                void SetupOptions (float maxTimeThreshold, float maxMovementThreshold)
                {
                    TapInputOptions.SetValues(new Dictionary<string, object>
                    {
                        { nameof(TapInputOptions.MaxTimeThreshold), maxTimeThreshold },
                        { nameof(TapInputOptions.MaxMovementThreshold), maxMovementThreshold }
                    });
                }
                
                [Test]
                public void Tap_Not_Performed_Distance_Exceeded ()
                {
                    Vector2? endPosition = null;
                    Model.OnTapPerformed += x => endPosition = x;
                    
                    SetupOptions(100f, 1);
                    
                    Model.PerformTap(Vector2.zero, new Vector2(200, 200), 1f);
                    
                    Assert.IsNull(endPosition);
                }
                
                [Test]
                public void Tap_Not_Performed_Duration_Exceeded ()
                {
                    Vector2? endPosition = null;
                    Model.OnTapPerformed += x => endPosition = x;
                    
                    SetupOptions(1f, 100f);
                    
                    Model.PerformTap(Vector2.zero, new Vector2(1, 1), 5f);
                    
                    Assert.IsNull(endPosition);
                }

                [Test]
                public void Assert_Tap_Performed ()
                {
                    Vector2? endPosition = Vector2.zero;
                    Model.OnTapPerformed += x => endPosition = x;
                    
                    SetupOptions(100f, 100f);
                    
                    Vector2 expectedEndPosition = new Vector2(1, 1);
                    Model.PerformTap(Vector2.zero, expectedEndPosition, 1f);
                    
                    Assert.IsNotNull(endPosition);
                    Assert.AreEqual(expectedEndPosition, endPosition.Value);
                }
            }
        }
    }
}