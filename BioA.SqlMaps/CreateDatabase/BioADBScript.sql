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