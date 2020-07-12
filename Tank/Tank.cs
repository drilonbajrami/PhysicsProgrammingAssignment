using GXPEngine;

// TODO: Fix this mess - see Assignment 2.2
class Tank : Sprite 
{
	// public fields & properties:
	public Vec2 position 
	{
		get 
		{
			return _position;
		}
	}
	public Vec2 velocity;

	// private fields:
	Vec2 _position;
	Barrel _barrel;

    float speed;
    float maxSpeed = 3.0f;
    float rotationSpeed = 0.3f;

	public Tank(float px, float py) : base("assets/bodies/t34.png") 
	{
        SetOrigin(width / 2, height / 2);
		_position.x = px;
		_position.y = py;
		_barrel = new Barrel ();
		AddChild (_barrel);
	}

	void Controls() 
	{
        if (Input.GetKey(Key.LEFT))
        {
            rotation -= speed * rotationSpeed;
        }
        if (Input.GetKey(Key.RIGHT))
        {
            rotation += speed * rotationSpeed;
        }
        if (Input.GetKey (Key.UP)) 
		{
            speed += 0.5f;
		}
		if (Input.GetKey (Key.DOWN)) 
		{
            speed -= 0.5f;
		}
        else
        {
            speed *= 0.9f;
        }

        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        } else if (speed < -maxSpeed)
        {
            speed = -maxSpeed;
        }

        velocity = Vec2.GetUnitVectorDeg(rotation);
        velocity.Normalize();
        velocity.Scale(speed);
    }

	void Shoot()
    {
        Vec2 delta = new Vec2(Input.mouseX - position.x, Input.mouseY - position.y);
        delta.Normalize();
        _barrel.rotation = delta.GetAngleDegrees() - rotation;
        
        
		if (Input.GetKeyDown (Key.SPACE) || Input.GetMouseButtonDown(0)) 
		{
            Vec2 bulletPosition = new Vec2(_position.x + _barrel.width * 2/3,position.y);
            Vec2 bulletVelocity = new Vec2(5, 0);
            bulletPosition.RotateAroundDegrees(position, delta.GetAngleDegrees());
            bulletVelocity.RotateDegrees(delta.GetAngleDegrees());   
            parent.AddChildAt(new Bullet (bulletPosition, bulletVelocity, delta.GetAngleDegrees()), 2);
		}
	}

	void UpdateScreenPosition() 
	{
		x = _position.x;
		y = _position.y;
	}

	public void Update() 
	{
		Controls ();
        // Basic Euler integration:
        _position.Add (velocity);
		Shoot();
		UpdateScreenPosition ();
	}
}
