// Враппер для unity3d

#ifdef MODELS_HPP
#else

	namespace UnityEngine {
		
		struct Rect {

			float x1;
			float y1;
			float x2;
			float y2;

			Rect(float x1, float y1, float x2, float y2) {
				this->x1 = x1;
				this->x2 = x2;
				this->y1 = y1;
				this->y2 = y2;
			}

			bool Intersect(const Vector2 &vector) {
				return vector.x<this->x1 || vector.y<this->y1 ||
					   vector.x>this->x2 || vector.y>this->y2;
			}

			bool Extersect(const Vector2 &vector) {
				return !Intersect(vector);
			}

		};

		struct Rect3D {

			float x1;
			float y1;
			float z1;
			float x2;
			float y2;
			float z2;

			bool Intersect(const Vector3 &vector) {
				return vector.x<x1 || vector.y<y1 || vector.z<z1 ||
					   vector.x>x2 || vector.y>y2 || vector.z>z2;
			}

			bool Extersect(const Vector3 &vector) {
				return !Intersect(vector);
			}

		};

		struct Vector3 {

			static Vector3 zero;
			static Vector3 one;

			float x = 0;
			float y = 0;
			float z = 0;

			Vector3(float x, float y, float z) {
				this->x = x;
				this->y = y;
				this->z = z;
			}

			Vector3* Create() const {
				return (Vector3*)malloc(sizeof(Vector3));
			}

			Vector3* Create(float x) {
				Vector3 *vector = Create();
					vector->x = x;
				return vector;
			}

			Vector3* Create(float x, float y) {
				Vector3 *vector = Create();
					vector->x = x;
					vector->y = y;
				return vector;
			}

			Vector3* Create(float x, float y, float z) {
				Vector3 *vector = Create();
					vector->x = x;
					vector->y = y;
					vector->z = z;
				return vector;
			}

			inline Vector3 operator/(const float &value) const {
				return { x / value, y / value, z / value };
			}

			inline Vector3 operator*(const float &value) const {
				return { x * value, y * value, z * value };
			}

			inline Vector3 operator-(const Vector3 &other) const {
				return { other.x - x, other.y - y, other.z - z };
			}

			inline Vector3 operator+(const Vector3 &other) const {
				return { other.x + x, other.y + y, other.z + z };
			}

			inline bool operator==(const Vector3 &other) const  {
				return (other.x == x && other.y == y && other.z == z);
			}

			inline bool operator!=(const Vector3 &other) const  {
				return !(*this == other);
			}

		};

		Vector3 Vector3::zero = Vector3(0,0,0);
		Vector3 Vector3::one  = Vector3(1, 1, 1);

		struct Vector2 {

			float x;
			float y;

			Vector2(float x, float y) {
				this->x = x;
				this->y = y;
			}

			Vector2* Create() {
				return (Vector2*)malloc(sizeof(Vector2));
			}

			Vector2* Create(float x) {
				Vector2 *vector = Create();
					vector->x = x;
				return vector;
			}

			Vector2* Create(float x, float y) {
				Vector2 *vector = Create();
					vector->x = x;
					vector->y = y;
				return vector;
			}

			inline Vector2 operator/(const float &value) const {
				return { x / value, y / value };
			}

			inline Vector2 operator*(const float &value) const {
				return { x * value, y * value };
			}

			inline Vector2 operator-(const Vector2 &other) const {
				return { other.x - x, other.y - y };
			}

			inline Vector2 operator+(const Vector2 &other) const {
				return { other.x + x, other.y + y };
			}

			inline bool operator==(const Vector2 &other) const {
				return (other.x == x && other.y == y);
			}

			inline bool operator!=(const Vector2 &other) const {
				return !(*this == other);
			}

		};

		

	}

#endif