namespace Final.Game.Casting
{
    /// <summary>
    /// A thing that participates in the game.
    /// </summary>
    public class Racket : Actor
    {
        private Body _body;
        private Body _topBody;
        private Body _bottomBody;
        private Animation _animation;
        
        /// <summary>
        /// Constructs a new instance of Actor.
        /// </summary>
        public Racket(Body body, Body topBody, Body bottomBody, Animation animation, bool debug) : base(debug)
        {
            this._body = body;
            this._topBody = topBody;
            this._bottomBody = bottomBody;
            this._animation = animation;
        }

        /// <summary>
        /// Gets the animation.
        /// </summary>
        /// <returns>The animation.</returns>
        public Animation GetAnimation()
        {
            return _animation;
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <returns>The body.</returns>
        public Body GetBody()
        {
            return _body;
        }
        public Body GetTopBody()
        {
            return _topBody;
        }
        public Body GetBottomBody()
        {
            return _bottomBody;
        }

        /// <summary>
        /// Moves the racket to its next position.
        /// </summary>
        public void MoveNext()
        {
            Point position = _body.GetPosition();
            Point velocity = _body.GetVelocity();
            Point newPosition = position.Add(velocity);
            _body.SetPosition(newPosition);
            _topBody.SetPosition(newPosition);
            _bottomBody.SetPosition(newPosition);
        }

        /// <summary>
        /// Swings the racket to the top.
        /// </summary>
        public void SwingUp()
        {
            Point velocity = new Point(0 ,-Constants.RACKET_VELOCITY);
            _body.SetVelocity(velocity);
            _topBody.SetVelocity(velocity);
            _bottomBody.SetVelocity(velocity);
        }

        /// <summary>
        /// Swings the racket to the bottom.
        /// </summary>
        public void SwingDown()
        {
            Point velocity = new Point(0 ,Constants.RACKET_VELOCITY);
            _body.SetVelocity(velocity);
            _topBody.SetVelocity(velocity);
            _bottomBody.SetVelocity(velocity);
        }

        /// <summary>
        /// Stops the racket from moving.
        /// </summary>
        public void StopMoving()
        {
            Point velocity = new Point(0, 0);
            _body.SetVelocity(velocity);
            _topBody.SetVelocity(velocity);
            _bottomBody.SetVelocity(velocity);
        }
    }
}