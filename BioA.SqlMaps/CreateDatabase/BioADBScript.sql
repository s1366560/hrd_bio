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






