function [WorkspaceIsInitialised] = CheckWorkspace()

if evalin( 'base', 'exist(''DepositionTime'',''var'') == 1' )
        WorkspaceIsInitialised = true;
else
    WorkspaceIsInitialised = false;
end
