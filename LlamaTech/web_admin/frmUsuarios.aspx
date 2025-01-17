﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmUsuarios.aspx.cs" Inherits="LlamaTech.web_admin.frmUsuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Admin Panel | Usuarios</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="css/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/startmin.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- Sweet Alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>

    <!-- Alerts -->
    <script src="../js/alerts.js"></script>
</head>
<body>
    <div id="wrapper">

        <form method="post" enctype="multipart/form-data" runat="server">
            <!-- Navigation -->
            <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
                <div class="navbar-header">
                    <a class="navbar-brand" href="frmPanelControl.aspx">Admin Panel</a>
                </div>

                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <ul class="nav navbar-nav navbar-left navbar-top-links">
                    <li><a href="../index.aspx" target="_blank"><i class="fa fa-home fa-fw"></i>Llamatech</a></li>
                </ul>

                <ul class="nav navbar-right navbar-top-links">
                    <li class="dropdown">
                        <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                            <i class="fa fa-user fa-fw"></i>
                            <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
                            <b class="caret"></b>
                        </a>
                        <ul class="dropdown-menu dropdown-user">
                            <li>
                                <asp:Button ID="btnCerrarSesion" CssClass="btn btn-link" runat="server" Text="Cerrar Sesión" OnClick="btnCerrarSesion_Click" />
                            </li>
                        </ul>
                    </li>
                </ul>
                <!-- /.navbar-top-links -->

                <div class="navbar-default sidebar" role="navigation">
                    <div class="sidebar-nav navbar-collapse">
                        <ul class="nav" id="side-menu">
                            <li>
                                <a href="frmPanelControl.aspx"><i class="fa fa-dashboard fa-fw"></i>Dashboard</a>
                            </li>
                            <li>
                                <a href="post.aspx"><i class="fa fa-edit fa-fw"></i>Entradas</a>
                            </li>
                            <li>
                                <a href="frmImages.aspx"><i class="fa fa-photo fa-fw"></i>Imagenes</a>
                            </li>
                            <li>
                                <a href="frmCategorias.aspx"><i class="fa fa-tags fa-fw"></i>Categorias</a>
                            </li>
                            <li>
                                <a href="frmUsuarios.aspx"><i class="fa fa-users fa-fw"></i>Usuarios</a>
                            </li>
                            <li>
                                <a href="frmHomePage.aspx"><i class="fa fa-home fa-fw"></i>Home Page</a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.sidebar-collapse -->
                </div>
            </nav>
            <!-- End Navigation -->

            <!-- Page Content -->
            <div id="page-wrapper">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
                            <h1 class="page-header">Usuarios</h1>
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- /.row -->
                    <!-- Form -->

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>ID</label>
                                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Width="100px" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Nombre</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Apellido</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Telefono</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Foto perfil</label>
                                <input type="file" id="fileImg" class="btnn" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Rol</label>
                                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Status</label>
                                <asp:CheckBox ID="chkStatus" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Image ID="imgUser" runat="server" CssClass="img-responsive" />
                                <label id="tamanioImg"></label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" Text="Guardar" />
                                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" />
                                <asp:Button ID="btnUpdatePass" runat="server" CssClass="btn btn-warning" Text="Modificar Contraseña" />
                                <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" />
                            </div>
                        </div>
                    </div>


                    <!-- CONFIRMAR CONTRASEÑA -->
                    <div class="modal" tabindex="-1" role="dialog" id="modalContraseñaGuardar" runat="server" style="overflow-y: auto;">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Agregar contraseña</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="inputPass">Contraseña</label>
                                                <input type="password" class="form-control" id="inputPass" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="inputPassRp">Confirmar Contraseña</label>
                                                <input type="password" class="form-control" id="inputPassRp" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAceptarGuardar" runat="server" Text="Aceptar" CssClass="btn btn-success" />
                                    <asp:Button ID="btnCancelarGuardar" runat="server" Text="Cancelar" CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- MODIFICAR CONTRASEÑA -->
                    <div class="modal" tabindex="-1" role="dialog" id="modalContraseniaModificar" runat="server" style="overflow-y: auto;">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title">Modificar contraseña</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="inputUpdatePass">Contraseña</label>
                                                <input type="password" class="form-control" id="inputUpdatePass" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="inputUpdatePassRp">Confirmar Contraseña</label>
                                                <input type="password" class="form-control" id="inputUpdatePassRp" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnAceptarEditarPass" runat="server" Text="Aceptar" CssClass="btn btn-success" />
                                    <asp:Button ID="btnAceptarCancelarPass" runat="server" Text="Cancelar" CssClass="btn btn-warning" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- End Form -->
                    <hr>
                    <!-- Table -->
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    Categorias
                                </div>
                                <!-- /.panel-heading -->
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <table class="table table-striped table-bordered table-hover" id="dt">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>ID</th>
                                                    <th>Nombre</th>
                                                    <th>Apellido</th>
                                                    <th>Telefono</th>
                                                    <th>Email</th>
                                                    <th>Foto de Perfil</th>
                                                    <th>Fecha Creación</th>
                                                    <th>Rol</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpGrid" runat="server">
                                                    <itemtemplate>
                                                    <tr class="odd gradeX">
                                                        <td><a href="#" class="select">Seleccionar</a></td>
                                                        <td><%# Eval("ID") %></td>
                                                        <td><%# Eval("Nombre") %></td>
                                                        <td><%# Eval("Apellido") %></td>
                                                        <td><%# Eval("Telefono") %></td>
                                                        <td><%# Eval("Email") %></td>
                                                        <td><%# Eval("FotoPerfil") %></td>
                                                        <td><%# Eval("FechaCreacion") %></td>
                                                        <td><%# Eval("Rol") %></td>
                                                        <td><%# Eval("Status") %></td>
                                                    </tr>
                                                </itemtemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- /.table-responsive -->
                                </div>
                                <!-- /.panel-body -->
                            </div>
                            <!-- /.panel -->
                        </div>
                        <!-- /.col-lg-12 -->
                    </div>
                    <!-- End Table -->
                </div>
                <!-- /.container-fluid -->
            </div>


            <!-- modal alertas -->
            <div class="modal" tabindex="-1" role="dialog" id="modalEditar" runat="server" style="overflow-y: auto;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Modificar</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p>¿Estas seguro que quieres editar?</p>
                        </div>
                        <div class="modal-footer">
                            <input type="button" id="btnAceptarMod" value="Aceptar" class="btn btn-success" />
                            <input type="button" id="btnCancelarMod" value="Aceptar" class="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- MODAL ELIMINAR -->
            <div class="modal" tabindex="-1" role="dialog" id="modalEliminar" runat="server" style="overflow-y: auto;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Eliminar</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p>¿Estas seguro que quieres eliminar la publicación?</p>
                        </div>
                        <div class="modal-footer">
                            <input type="button" id="btnAceptarEliminar" value="Aceptar" class="btn btn-success" />
                            <input type="button" id="btnCancelarEliminar" value="Aceptar" class="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </div>

        </form>
    </div>
    <!-- jQuery -->
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/metisMenu.min.js"></script>

    <!-- DataTables JavaScript -->
    <script src="js/dataTables/jquery.dataTables.min.js"></script>
    <script src="js/dataTables/dataTables.bootstrap.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/startmin.js"></script>

    <!-- JS USERS -->
    <script src="js/usuarios.js"></script>

</body>
</html>
