DROP PROCEDURE IF EXISTS DeleteAssayProject;  
DELIMITER //
create procedure DeleteAssayProject(IN proName text, IN proType text, OUT n int)
BEGIN
	declare projectCount int default 0;
    declare combNum int default 0; 
    set n = 0;
    select count(*) into projectCount from QCRelationProjectTb where ProjectName=proName and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into projectCount from CalibratorProjectTb where ProjectName=proName and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into projectCount from calcprojectinfotb where CalcFormula like concat('%',proName,'%') and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into combNum from assayprojectinfotb where ProjectName=proName;
    if combNum = 1
    then
		begin
			select count(*) into projectCount from combprojectinfotb where ProjectName=proName;
			set n = n + projectCount;
		end;
	end if;
    
    if n = 0
    then
		begin
			delete from AssayProjectParamInfoTb where ProjectName=proName and SampleType=proType;
			delete from CalibrationParamInfoTb where ProjectName=proName and SampleType=proType;
			delete from RangeParamInfoTb where ProjectName=proName and SampleType=proType;
			delete from assayprojectinfotb where ProjectName=proName and SampleType=proType;
			delete from projectrunsequencetb where ProjectName=proName and SampleType=proType;
		end;
    end if;
	select n;

END;
//
DELIMITER ;

DROP PROCEDURE IF EXISTS UpdateAssayProject;  
DELIMITER //
create procedure UpdateAssayProject(IN proModifyName text, IN proproModifyType text, IN proFullName text, IN channelNum text, IN proOldName text, IN proOldType text, OUT n int)
BEGIN
	declare projectCount int default 0;
    declare combNum int default 0; 
    set n = 0;
    select count(*) into projectCount from QCRelationProjectTb where ProjectName=proName and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into projectCount from CalibratorProjectTb where ProjectName=proName and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into projectCount from calcprojectinfotb where CalcFormula like concat('%',proName,'%') and SampleType=proType;
    if projectCount > 0
    then
		set n = n + projectCount;
	end if;
	select count(*) into combNum from assayprojectinfotb where ProjectName=proName;
    if combNum = 1
    then
		begin
			select count(*) into projectCount from combprojectinfotb where ProjectName=proName;
			set n = n + projectCount;
		end;
	end if;
    
    if n = 0
    then
    begin
		update AssayProjectParamInfoTb set ProjectName = proModifyName, SampleType = proproModifyType where ProjectName=proOldName and SampleType=proOldType;
		update CalibrationParamInfoTb set ProjectName = proModifyName, SampleType = proproModifyType where ProjectName=proOldName and SampleType=proOldType;
		update RangeParamInfoTb set ProjectName = proModifyName, SampleType = proproModifyType where ProjectName=proOldName and SampleType=proOldType;
        update assayprojectinfotb set ProjectName = proModifyName, SampleType = proproModifyType, ProFullName = proFullName, ChannelNum = channelNum where ProjectName=proOldName and SampleType=proOldType;
		update ProjectRunSequenceTb set ProjectName=proModifyName, SampleType=proModifyType where ProjectName=proOldName and SampleType=proOldType;
	end;
    end if;
	select n;

END;
//
DELIMITER ;

-- 删除校准品项目信息和校准品信息 
USE `bioadb`;
DROP procedure IF EXISTS `DeleteCalibratorProjectRI`;

DELIMITER $$
USE `bioadb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteCalibratorProjectRI`(IN NumTime int, IN calibratorName varchar(50), IN proName varchar(50), in samType varchar(50),  OUT n int)
BEGIN
	declare counts int default 0;
    set n = 0;
    if NumTime = 0 then
		begin
			select count(*) into counts from calibratorprojecttb where CalibName=calibratorName;
            set n = counts;
            if n > 0 then
				begin
					delete from calibratorprojecttb where CalibName=calibratorName;
                    select count(*) into counts from calibratortb where CalibName=calibratorName;
					if counts > 0 then
						begin
							delete from calibratortb where CalibName=calibratorName;
						end;
					end if;
                end;
			end if;
			
        end;
	end if;
	select count(*) into counts from calibrationparaminfotb where ProjectName=proName and SampleType=samType and CalibrationMethod is not null;
	if counts > 0
		then
			set n = n+ counts;
			begin
				delete from calibrationparaminfotb where ProjectName=proName and SampleType=samType;
				insert into CalibrationParamInfoTb (ProjectName, SampleType) values(proName, samType);
			end;
	end if;
    select n;
END$$

DELIMITER ;

-- 保存项目信息、项目参数信息、校准项目参数信息、范围参数信息、项目运行顺序信息
USE `bioadb`;
DROP procedure IF EXISTS `SaveAssayProjectInfoAll`;

DELIMITER $$
USE `bioadb`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SaveAssayProjectInfoAll`(in proName varchar(50), in samType varchar(50), in projectFullName varchar(100), in NCHAN varchar(10), OUT n int)
BEGIN
	declare counts int default 0;
	set n = 0;
    select count(*) into counts from AssayProjectInfoTb where ProjectName=proName and SampleType=samType;
    set n = counts;
    select count(*) into counts from AssayProjectParamInfoTb where ProjectName=proName and SampleType=samType;
    set n = n + counts;
    select count(*) into counts from CalibrationParamInfoTb where ProjectName=proName and SampleType=samType;
    set n = n +counts;
    select count(*) into counts from RangeParamInfoTb where ProjectName=proName and SampleType=samType;
    set n = n +counts;
    select count(*) into counts from ProjectRunSequenceTb where ProjectName=proName and SampleType=samType;
    set n = n +counts;
    if n = 0 then
		begin
			insert into AssayProjectInfoTb (ProjectName, SampleType, ProFullName, ChannelNum)
				values (proName, samType, projectFullName, NCHAN);
                
			insert into AssayProjectParamInfoTb (ProjectName, SampleType) values (proName, samType);
            
            insert into CalibrationParamInfoTb (ProjectName, SampleType) values(proName, samType);
            
			insert into RangeParamInfoTb
			  (ProjectName, SampleType, AgeLow1, AgeHigh1, ManConsLow1, ManConsHigh1, WomanConsLow1, WomanConsHigh1,
			  AgeLow2, AgeHigh2, ManConsLow2, ManConsHigh2, WomanConsLow2, WomanConsHigh2,
			  AgeLow3, AgeHigh3, ManConsLow3, ManConsHigh3, WomanConsLow3, WomanConsHigh3,
			  AgeLow4, AgeHigh4, ManConsLow4, ManConsHigh4, WomanConsLow4, WomanConsHigh4)
			  values(proName, samType, -100000000, 100000000, -100000000, 100000000, -100000000, 100000000,
			  -100000000, 100000000, -100000000, 100000000, -100000000, 100000000,
			  -100000000, 100000000, -100000000, 100000000, -100000000, 100000000,
			  -100000000, 100000000, -100000000, 100000000, -100000000, 100000000);
              
			insert into ProjectRunSequenceTb values(proName, samType,-1);
		end;
    end if;
    select n; 
END$$

DELIMITER ;







