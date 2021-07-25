using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using WebApiCSharp.Services;
using WebApiCSharp.Models;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApiCSharp.GenerateCodeFiles
{
    public class GenerateRosMiddleware
    {
        public const string ROS_MIDDLEWARE_PACKAGE_NAME = "aos_ros_middleware_auto";

        private static Configuration conf;
        static GenerateRosMiddleware()
        {
            conf = ConfigurationService.Get();
        }



        public GenerateRosMiddleware(PLPsData data, InitializeProject initProj)
        {
            string rosWorkspaceSrcDirPath = GenerateFilesUtils.AppendPath(initProj.RosTarget.WorkspaceDirectortyPath, "src");
            string rosMiddlewareDirectory = GenerateFilesUtils.AppendPath(rosWorkspaceSrcDirPath, ROS_MIDDLEWARE_PACKAGE_NAME);

            GenerateFilesUtils.DeleteAndCreateDirectory(rosMiddlewareDirectory, false);


            GenerateFilesUtils.RunApplicationUntilEnd("catkin_create_pkg", rosWorkspaceSrcDirPath, ROS_MIDDLEWARE_PACKAGE_NAME + " std_msgs rospy roscpp");

            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/CMakeLists.txt", RosMiddlewareFileTemplate.GetCMakeListsFile());

            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/package.xml", RosMiddlewareFileTemplate.GetPackageFile(initProj));

            Directory.CreateDirectory(rosMiddlewareDirectory + "/scripts");
            GenerateFilesUtils.WriteTextFile(rosMiddlewareDirectory + "/scripts/" + ROS_MIDDLEWARE_PACKAGE_NAME + "_node.py", RosMiddlewareFileTemplate.GetAosRosMiddlewareNodeFile(data, initProj));





        }


    }
}