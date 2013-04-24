using GoblinXNA.Graphics;
using GoblinXNA.Graphics.Geometry;
using GoblinXNA.Helpers;
using GoblinXNA.SceneGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model = GoblinXNA.Graphics.Model;


namespace Slime_Engine
{
    class AR_Object
    {
        // The GeometryNode to be used by GoblinXNA
        GeometryNode geomNode;

        // The TransformationNode to be used to manipulate the tank
        TransformNode transNode;

        // Keep track of what kind of object we are
        enum objectType { model, geometry };
        int objType;

        // Keep track of teh rotational details
        Quaternion objRotation;

        // Keep track of whether or not this item is currently in a selected state
        bool selected;

        // Keep track of which coordinate system it lives in
        int coordSystem;

        // Limits on translation
        const int MAX_X = 100;
        const int MIN_X = -100;
        const int MAX_Y = 100;
        const int MIN_Y = -100;
        const int MAX_Z = 50;
        const int MIN_Z = -50;

        public AR_Object(string name)
        {
            coordSystem = 0;
            if (name.Equals("tank") || name.Equals("Ship"))
            {
                objType = (int)objectType.model;
                load(name);
            }
            else if (name.Equals("cube") || name.Equals("sphere"))
            {
                objType = (int)objectType.geometry;
                create(name);
            }
            else
            {
                throw new Exception("Unknown name for AR_Object creation");
            }

            // Add physics properties
            geomNode.Physics.Shape = GoblinXNA.Physics.ShapeType.ConvexHull;
            //geomNode.Physics.Pickable = true;
            geomNode.Physics.Collidable = true;
            geomNode.AddToPhysicsEngine = true;

            objRotation = Quaternion.Identity;
            selected = false;

            // Apply the transformations
            transNode = new TransformNode();
            transNode.AddChild(geomNode);
        }

        public TransformNode getTransformNode()
        {
            return transNode;
        }

        public int getCoordSystem()
        {
            return coordSystem;
        }

        public void setCoordSystem(int newCoordSys)
        {
            coordSystem = newCoordSys;
        }

        private void create(string name)
        {
            geomNode = new GeometryNode(name);

            if (name.Equals("cone"))
            {
                geomNode.Model = new Cylinder(1.5f, 0, 4f, 20);

                // Create a material to apply to the box model
                Material coneMaterial = new Material();
                coneMaterial.Diffuse = Color.Tomato.ToVector4();
                coneMaterial.Specular = Color.White.ToVector4();
                coneMaterial.SpecularPower = 5;

                geomNode.Material = coneMaterial;
            }
            else if (name.Equals("sphere"))
            {
                geomNode.Model = new Sphere(1, 20, 20);

                // Create a material to apply to the ball
                Material ballMaterial = new Material();
                ballMaterial.Diffuse = Color.SteelBlue.ToVector4();
                ballMaterial.Specular = Color.White.ToVector4();
                ballMaterial.SpecularPower = 5;

                geomNode.Material = ballMaterial;
            }
        }

        private void load(string name)
        {
            ModelLoader loader = new ModelLoader();
            geomNode = new GeometryNode(name);
            geomNode.Model = (Model)loader.Load("", name);
            ((Model)geomNode.Model).UseInternalMaterials = true;
        }

        public void select()
        {
            if (selected)
                return;
            selected = true;
            if (objType == (int)objectType.geometry)
            {
                geomNode.Model.ShowBoundingBox = true;
            }
            else if (objType == (int)objectType.model)
            {
                geomNode.Model.ShowBoundingBox = true;
            }
            else
            {
                throw new Exception("Unknown object type for selection");
            }
        }

        public void deselect()
        {
            if (!selected)
                return;
            selected = false;
            if (objType == (int)objectType.geometry)
            {
                geomNode.Model.ShowBoundingBox = false;
            }
            else if (objType == (int)objectType.model)
            {
                geomNode.Model.ShowBoundingBox = false;
            }
            else
            {
                throw new Exception("Unknown object type for deselection");
            }
        }

        public string translationToString()
        {
            return "X:" + transNode.Translation.X.ToString() + " Y:" + transNode.Translation.Y.ToString() + " Z:" + transNode.Translation.Z.ToString();
        }

        public Vector3 getDimensions()
        {
            return Vector3Helper.GetDimensions(geomNode.Model.MinimumBoundingBox);
        }

        public void scaleToSize(float size)
        {
            float scale = 1f;
            Vector3 dimensions = getDimensions();
            if (dimensions.X > dimensions.Y)
            {
                scale = size / Math.Max(dimensions.X, dimensions.Z);
            }
            else
            {
                scale = size / Math.Max(dimensions.Y, dimensions.Z);
            }
            transNode.Scale = new Vector3(scale, scale, scale);
        }

        public void translate(Vector3 translateAmounts)
        {
            float oldX = transNode.Translation.X;
            float oldY = transNode.Translation.Y;
            float oldZ = transNode.Translation.Z;
            float newX = translateAmounts.X;
            float newY = translateAmounts.Y;
            float newZ = translateAmounts.Z;
            newX += oldX;
            newY += oldY;
            newZ += oldZ;
            Vector3 newTrans = new Vector3(newX, newY, newZ);
            transNode.Translation = newTrans;
        }

        public void setTranslationOneTime(Vector3 newTranslation)
        {
            transNode.Translation = newTranslation;
        }

        public void setTranslationWeighted(Vector3 newTranslation)
        {
            newTranslation = weightChange(newTranslation);
            transNode.Translation = newTranslation;
        }

        private Vector3 weightChange(Vector3 change)
        {
            float x = (transNode.Translation.X * 9f / 10f) + (change.X / 10f);
            float y = (transNode.Translation.Y * 9f / 10f) + (change.Y / 10f);
            float z = (transNode.Translation.Z * 9f / 10f) + (change.Z / 10f);
            return new Vector3(x, y, z);
        }

        public void setRotatation(Quaternion newRotation)
        {
            objRotation = newRotation;
            updateRotation();
        }

        private void updateRotation()
        {
            transNode.Rotation = objRotation;
        }

        #region Translation Incrementors

        public void stepInX(float stepSize, bool reverse)
        {
            if (reverse)
                translate(new Vector3(-1 * stepSize, 0, 0));
            else
                translate(new Vector3(stepSize, 0, 0));
        }
        public void stepInY(float stepSize, bool reverse)
        {
            if (reverse)
                translate(new Vector3(0, -1 * stepSize, 0));
            else
                translate(new Vector3(0, stepSize, 0));
        }
        public void stepInZ(float stepSize, bool reverse)
        {
            if (reverse)
                translate(new Vector3(0, 0, -1 * stepSize));
            else
                translate(new Vector3(0, 0, stepSize));
        }

        #endregion

        #region Scale Incrementors

        public void scaleInX(float stepSize, bool reverse)
        {
            if (reverse)
                transNode.Scale -= new Vector3(stepSize, 0, 0);
            else
                transNode.Scale += new Vector3(stepSize, 0, 0);
        }
        public void scaleInY(float stepSize, bool reverse)
        {
            if (reverse)
                transNode.Scale -= new Vector3(0, stepSize, 0);
            else
                transNode.Scale += new Vector3(0, stepSize, 0);
        }
        public void scaleInZ(float stepSize, bool reverse)
        {
            if (reverse)
                transNode.Scale -= new Vector3(0, 0, stepSize);
            else
                transNode.Scale += new Vector3(0, 0, stepSize);
        }
        public void scaleProportional(float stepSize, bool reverse)
        {
            if (reverse)
                transNode.Scale -= new Vector3(stepSize);
            else
                transNode.Scale += new Vector3(stepSize);
        }

        #endregion

        #region Rotation Incrementors

        public void rotateInX(float stepSize, bool reverse)
        {
            if (reverse)
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitX, -1 * stepSize);
            else
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitX, stepSize);
            updateRotation();
        }
        public void rotateInY(float stepSize, bool reverse)
        {
            if (reverse)
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitZ, -1 * stepSize);
            else
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitZ, stepSize);
            updateRotation();
        }
        public void rotateInZ(float stepSize, bool reverse)
        {
            if (reverse)
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitY, -1 * stepSize);
            else
                objRotation *= Quaternion.CreateFromAxisAngle(Vector3.UnitY, stepSize);
            updateRotation();
        }

        #endregion
    }
}
