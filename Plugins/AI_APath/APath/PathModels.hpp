#ifdef PATH_MODELS_HPP
#else

#include "UnityEngine.hpp"

using UnityEngine::Vector3;

	namespace APath {

		class Point {

		private:

			Point   *startPoint = nullptr;
			Vector3 *endPoint   = nullptr;
			float   *length     = nullptr;

		public:

			Point(Point *start, Vector3 *end, float *length);

			Vector3* getEndPoint() {
				return endPoint;
			}

			Vector3* getStartPoint() {
				return startPoint == nullptr ? nullptr : startPoint->endPoint;
			}

		};

		Point::Point(Point *start, Vector3 *end, float *length) {
			this->startPoint = start;
			this->endPoint   = end;
			this->length     = length;
		}

	}

#endif