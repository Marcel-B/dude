export interface ProjectsProps {
  triggerSnackbar: (message: string, severity: "success" | "error" | "info") => void;
}
export const Projects = ({triggerSnackbar}: ProjectsProps) => {

  return (<>Projects</>);
}
