import arcpy
# 获取 ArCGIS 安装信息
install_info = arcpy.GetInstallInfo()
arcgis_version =install_info['Version']
# 输出版本信息
arcpy.AddMessage(arcgis_version)
